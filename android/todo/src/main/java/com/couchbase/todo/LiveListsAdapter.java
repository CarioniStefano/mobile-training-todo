package com.couchbase.todo;

import android.content.Context;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ArrayAdapter;
import android.widget.TextView;

import com.couchbase.lite.DataSource;
import com.couchbase.lite.Database;
import com.couchbase.lite.Document;
import com.couchbase.lite.Expression;
import com.couchbase.lite.LiveQuery;
import com.couchbase.lite.LiveQueryChange;
import com.couchbase.lite.LiveQueryChangeListener;
import com.couchbase.lite.Log;
import com.couchbase.lite.Ordering;
import com.couchbase.lite.Query;
import com.couchbase.lite.Result;
import com.couchbase.lite.ResultSet;
import com.couchbase.lite.SelectResult;

public class LiveListsAdapter extends ArrayAdapter<String> {
    private static final String TAG = LiveListsAdapter.class.getSimpleName();

    private Database db;
    private LiveQuery query;

    public LiveListsAdapter(Context context, Database db) {
        super(context, 0);
        this.db = db;

        this.query = query();
        this.query.addChangeListener(new LiveQueryChangeListener() {
            @Override
            public void changed(LiveQueryChange change) {
                clear();
                ResultSet rs = change.getRows();
                Result result;
                while ((result = rs.next()) != null) {
                    add(result.getString(0));
                }
                notifyDataSetChanged();
            }
        });
        this.query.run();
    }

    @Override
    public View getView(int position, View convertView, ViewGroup parent) {
        String id = getItem(position);
        Document list = db.getDocument(id);
        if (convertView == null)
            convertView = LayoutInflater.from(getContext()).inflate(R.layout.view_list, parent, false);
        TextView text = convertView.findViewById(R.id.text);
        text.setText(list.getString("name"));
        Log.e(TAG, "getView(): pos -> %d, docID -> %s, name -> %s, name2 -> %s, all -> %s", position, list.getId(), list.getString("name"), list.getObject("name"), list.toMap());
        return convertView;
    }

    private LiveQuery query() {
        return Query.select(SelectResult.expression(Expression.meta().getId()))
                .from(DataSource.database(db))
                .where(Expression.property("type").equalTo("task-list"))
                .orderBy(Ordering.property("name").ascending())
                .toLive();
    }
}
