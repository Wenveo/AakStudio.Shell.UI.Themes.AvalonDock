﻿/************************************************************************
   AvalonDock

   Copyright (C) 2007-2013 Xceed Software Inc.

   This program is provided to you under the terms of the Microsoft Public
   License (Ms-PL) as published at https://opensource.org/licenses/MS-PL
 ************************************************************************/

using AvalonDock.Controls;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace AvalonDock.Themes.VisualStudio.Controls
{
    /// <inheritdoc />
    /// <summary>
    /// Implements a button that is used to display a <see cref="ContextMenuEx"/>
    /// when the user clicks on it
    /// (see templates for <see cref="LayoutAnchorableFloatingWindowControl"/>,
    /// <see cref="AnchorablePaneTitle"/>, and <see cref="LayoutDocumentPaneControl"/>).
    /// </summary>
    /// <seealso cref="ToggleButton"/>
    public class DropDownButton : ToggleButton
    {
        #region Properties

        #region DropDownContextMenu

        /// <summary><see cref="DropDownContextMenu"/> dependency property.</summary>
        public static readonly DependencyProperty DropDownContextMenuProperty = DependencyProperty.Register(nameof(DropDownContextMenu), typeof(ContextMenu), typeof(DropDownButton),
                new FrameworkPropertyMetadata(null, OnDropDownContextMenuChanged));

        /// <summary>Gets/sets the drop down menu to show up when user click on an anchorable menu pin.</summary>
        [Bindable(true), Description("Gets/sets the drop down menu to show up when user click on an anchorable menu pin."), Category("Menu")]
        public ContextMenu DropDownContextMenu
        {
            get => (ContextMenu)GetValue(DropDownContextMenuProperty);
            set => SetValue(DropDownContextMenuProperty, value);
        }

        /// <summary>Handles changes to the <see cref="DropDownContextMenu"/> property.</summary>
        private static void OnDropDownContextMenuChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((DropDownButton)d).OnDropDownContextMenuChanged(e);
        }

        /// <summary>Provides derived classes an opportunity to handle changes to the <see cref="DropDownContextMenu"/> property.</summary>
        protected virtual void OnDropDownContextMenuChanged(DependencyPropertyChangedEventArgs e)
        {
            if (e.OldValue is ContextMenu oldContextMenu && IsChecked.GetValueOrDefault())
                oldContextMenu.Closed -= OnContextMenuClosed;
        }

        #endregion DropDownContextMenu

        #region DropDownContextMenuDataContext

        /// <summary><see cref="DropDownContextMenuDataContext"/> dependency property.</summary>
        public static readonly DependencyProperty DropDownContextMenuDataContextProperty = DependencyProperty.Register(nameof(DropDownContextMenuDataContext), typeof(object), typeof(DropDownButton),
                new FrameworkPropertyMetadata(null));

        /// <summary>Gets/sets the DataContext to set for the DropDownContext menu property.</summary>
        [Bindable(true), Description("Gets/sets the DataContext to set for the DropDownContext menu property."), Category("Menu")]
        public object DropDownContextMenuDataContext
        {
            get => GetValue(DropDownContextMenuDataContextProperty);
            set => SetValue(DropDownContextMenuDataContextProperty, value);
        }

        #endregion DropDownContextMenuDataContext

        #endregion Properties

        #region Overrides

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            if (DropDownContextMenu != null)
            {
                /* Active Item */
                if (TemplatedParent is AnchorablePaneTitle anchorablePaneTitle)
                {
                    anchorablePaneTitle.Model.IsActive = true;
                }
                DropDownContextMenu.PlacementTarget = this;
                DropDownContextMenu.Placement = PlacementMode.Bottom;
                DropDownContextMenu.DataContext = DropDownContextMenuDataContext;
                DropDownContextMenu.Closed += OnContextMenuClosed;
                DropDownContextMenu.IsOpen = true;

                /* Focus */
                IsChecked = true;
                /* Fix Drag Error */
                e.Handled = true;
            }
            /* Disabled Parent Method */
            //base.OnMouseLeftButtonDown(e);
        }

        #endregion Overrides

        #region Private Methods

        private void OnContextMenuClosed(object sender, RoutedEventArgs e)
        {
            //Debug.Assert(IsChecked.GetValueOrDefault());
            if (sender is ContextMenu contextMenu)
            {
                contextMenu.Closed -= OnContextMenuClosed;
                IsChecked = false;
            }
        }
        #endregion Private Methods
    }
}
