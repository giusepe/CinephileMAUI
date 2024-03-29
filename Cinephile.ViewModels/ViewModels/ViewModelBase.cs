﻿// Copyright (c) 2019 .NET Foundation and Contributors. All rights reserved.
// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for full license information.

using System.Reactive;
using Cinephile.Infrastructure.Framework.Scheduler;
using ReactiveUI;

namespace Cinephile.ViewModels.ViewModels
{
    /// <summary>
    /// A base for all the different view models used throughout the application.
    /// </summary>
    public abstract class ViewModelBase : ReactiveObject, IActivatableViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ViewModelBase"/> class.
        /// </summary>
        protected ViewModelBase(ISchedulerService schedulerService)
        {
            SchedulerService = schedulerService;

            ShowAlert = new Interaction<AlertViewModel, Unit>(SchedulerService.MainThreadScheduler);
            OpenBrowser = new Interaction<string, Unit>(SchedulerService.MainThreadScheduler);
        }

        /// <summary>
        /// Gets the activator which contains context information for use in activation of the view model.
        /// </summary>
        public ViewModelActivator Activator { get; } = new ViewModelActivator();

        /// <summary>
        /// Gets a interaction which will show an alert.
        /// </summary>
        public Interaction<AlertViewModel, Unit> ShowAlert { get; }

        /// <summary>
        /// Gets an interaction which will open a browser window.
        /// </summary>
        public Interaction<string, Unit> OpenBrowser { get; }

        protected ISchedulerService SchedulerService { get; }
    }
}
