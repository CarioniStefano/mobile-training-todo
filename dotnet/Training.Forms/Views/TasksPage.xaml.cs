﻿//
// TasksPage.xaml.cs
//
// Author:
// 	Jim Borden  <jim.borden@couchbase.com>
//
// Copyright (c) 2016 Couchbase, Inc All rights reserved.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//

using MvvmCross.Forms.Views;
using Training.Core;
using Xamarin.Forms;

namespace Training
{
    /// <summary>
    /// The page that display the tasks in a given task list
    /// </summary>
    public partial class TasksPage : MvxContentPage
    {

        #region Constructors

        /// <summary>
        /// Constructor
        /// </summary>
        public TasksPage()
        {
            InitializeComponent();
        }

        private void OnUsers_Clicked(object sender, System.EventArgs e)
        {

        }

        private void OnEdit_Clicked(object sender, System.EventArgs e)
        {
            var param = ((MenuItem)sender).CommandParameter;
            var data = ((MenuItem)sender).BindingContext as TaskCellModel;
            data.StatusUpdated += UpdateView;
            data.EditCommand.Execute(param);
        }

        private void OnDelete_Clicked(object sender, System.EventArgs e)
        {
            var param = ((MenuItem)sender).CommandParameter;
            var data = ((MenuItem)sender).BindingContext as TaskCellModel;
            data.StatusUpdated += UpdateView;
            data.DeleteCommand.Execute(param);
        }

        protected void UpdateView()
        {
            var viewModel = DataContext as TasksViewModel;
            viewModel.Model.Filter(null);
        }

        #endregion

    }
}

