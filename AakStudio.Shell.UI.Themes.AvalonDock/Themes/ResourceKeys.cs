namespace AakStudio.Shell.UI.Themes.AvalonDock.Themes
{

    /* 项目“AakStudio.Shell.UI.Themes.AvalonDock (net5.0-windows)”的未合并的更改
    在此之前:
        using System.Windows;
    在此之后:
        using System.Windows;
        using AvalonDock;
        using AvalonDock.Themes;
        using AakStudio.Shell.UI.Themes.AvalonDock.Themes;
        using AakStudio.Shell.UI.Themes.AvalonDock.Themes;
    */
    using System.Windows;

    /// <summary>
    /// Resource key management class to keep track of all resources
    /// that can be re-styled in applications that make use of the implemented controls.
    /// </summary>
    public static class ResourceKeys
    {
        #region Accent Keys
        /// <summary>
        /// Accent Color Key - This Color key is used to accent elements in the UI
        /// (e.g.: Color of Activated Normal Window Frame, ResizeGrip, Focus or MouseOver input elements)
        /// </summary>
        public static readonly ComponentResourceKey ControlAccentColorKey = new ComponentResourceKey(typeof(ResourceKeys), "ControlAccentColorKey");

        /// <summary>
        /// Accent Brush Key - This Brush key is used to accent elements in the UI
        /// (e.g.: Color of Activated Normal Window Frame, ResizeGrip, Focus or MouseOver input elements)
        /// </summary>
        public static readonly ComponentResourceKey ControlAccentBrushKey = new ComponentResourceKey(typeof(ResourceKeys), "ControlAccentBrushKey");
        #endregion Accent Keys

        #region Brush Keys
        // General
        public static readonly ComponentResourceKey Background = new ComponentResourceKey(typeof(ResourceKeys), "Background");
        public static readonly ComponentResourceKey PanelBorderBrush = new ComponentResourceKey(typeof(ResourceKeys), "PanelBorderBrush");
        public static readonly ComponentResourceKey TabBackground = new ComponentResourceKey(typeof(ResourceKeys), "TabBackground");

        // Auto Hide : Tab
        public static readonly ComponentResourceKey AutoHideTabDefaultBackground = new ComponentResourceKey(typeof(ResourceKeys), "AutoHideTabDefaultBackground");
        public static readonly ComponentResourceKey AutoHideTabDefaultBorder = new ComponentResourceKey(typeof(ResourceKeys), "AutoHideTabDefaultBorder");
        public static readonly ComponentResourceKey AutoHideTabDefaultText = new ComponentResourceKey(typeof(ResourceKeys), "AutoHideTabDefaultText");

        // Mouse Over Auto Hide Button for (collapsed) Auto Hidden Elements
        public static readonly ComponentResourceKey AutoHideTabHoveredBackground = new ComponentResourceKey(typeof(ResourceKeys), "AutoHideTabHoveredBackground");
        // AccentColor
        public static readonly ComponentResourceKey AutoHideTabHoveredBorder = new ComponentResourceKey(typeof(ResourceKeys), "AutoHideTabHoveredBorder");
        // AccentColor
        public static readonly ComponentResourceKey AutoHideTabHoveredText = new ComponentResourceKey(typeof(ResourceKeys), "AutoHideTabHoveredText");

        // Document Well : Overflow Button
        public static readonly ComponentResourceKey DocumentWellOverflowButtonDefaultGlyph = new ComponentResourceKey(typeof(ResourceKeys), "DocumentWellOverflowButtonDefaultGlyph");
        public static readonly ComponentResourceKey DocumentWellOverflowButtonHoveredBackground = new ComponentResourceKey(typeof(ResourceKeys), "DocumentWellOverflowButtonHoveredBackground");
        public static readonly ComponentResourceKey DocumentWellOverflowButtonHoveredBorder = new ComponentResourceKey(typeof(ResourceKeys), "DocumentWellOverflowButtonHoveredBorder");
        // AccentColor
        public static readonly ComponentResourceKey DocumentWellOverflowButtonHoveredGlyph = new ComponentResourceKey(typeof(ResourceKeys), "DocumentWellOverflowButtonHoveredGlyph");
        // AccentColor
        public static readonly ComponentResourceKey DocumentWellOverflowButtonPressedBackground = new ComponentResourceKey(typeof(ResourceKeys), "DocumentWellOverflowButtonPressedBackground");
        public static readonly ComponentResourceKey DocumentWellOverflowButtonPressedBorder = new ComponentResourceKey(typeof(ResourceKeys), "DocumentWellOverflowButtonPressedBorder");
        public static readonly ComponentResourceKey DocumentWellOverflowButtonPressedGlyph = new ComponentResourceKey(typeof(ResourceKeys), "DocumentWellOverflowButtonPressedGlyph");

        // Document Well : Tab
        // Selected Document Highlight Header Top color (AccentColor)
        public static readonly ComponentResourceKey DocumentWellTabSelectedActiveBackground = new ComponentResourceKey(typeof(ResourceKeys), "DocumentWellTabSelectedActiveBackground");

        public static readonly ComponentResourceKey DocumentWellTabSelectedActiveText = new ComponentResourceKey(typeof(ResourceKeys), "DocumentWellTabSelectedActiveText");
        public static readonly ComponentResourceKey DocumentWellTabSelectedInactiveBackground = new ComponentResourceKey(typeof(ResourceKeys), "DocumentWellTabSelectedInactiveBackground");
        public static readonly ComponentResourceKey DocumentWellTabSelectedInactiveText = new ComponentResourceKey(typeof(ResourceKeys), "DocumentWellTabSelectedInactiveText");
        public static readonly ComponentResourceKey DocumentWellTabUnselectedBackground = new ComponentResourceKey(typeof(ResourceKeys), "DocumentWellTabUnselectedBackground");
        public static readonly ComponentResourceKey DocumentWellTabUnselectedText = new ComponentResourceKey(typeof(ResourceKeys), "DocumentWellTabUnselectedText");
        // AccentColor
        public static readonly ComponentResourceKey DocumentWellTabUnselectedHoveredBackground = new ComponentResourceKey(typeof(ResourceKeys), "DocumentWellTabUnselectedHoveredBackground");
        public static readonly ComponentResourceKey DocumentWellTabUnselectedHoveredText = new ComponentResourceKey(typeof(ResourceKeys), "DocumentWellTabUnselectedHoveredText");

        // Document Well : Tab : Button
        public static readonly ComponentResourceKey DocumentWellTabButtonSelectedActiveGlyph = new ComponentResourceKey(typeof(ResourceKeys), "DocumentWellTabButtonSelectedActiveGlyph");

        // AccentColor
        public static readonly ComponentResourceKey DocumentWellTabButtonSelectedActiveHoveredBackground = new ComponentResourceKey(typeof(ResourceKeys), "DocumentWellTabButtonSelectedActiveHoveredBackground");
        // AccentColor
        public static readonly ComponentResourceKey DocumentWellTabButtonSelectedActiveHoveredBorder = new ComponentResourceKey(typeof(ResourceKeys), "DocumentWellTabButtonSelectedActiveHoveredBorder");
        public static readonly ComponentResourceKey DocumentWellTabButtonSelectedActiveHoveredGlyph = new ComponentResourceKey(typeof(ResourceKeys), "DocumentWellTabButtonSelectedActiveHoveredGlyph");
        // AccentColor
        public static readonly ComponentResourceKey DocumentWellTabButtonSelectedActivePressedBackground = new ComponentResourceKey(typeof(ResourceKeys), "DocumentWellTabButtonSelectedActivePressedBackground");
        // AccentColor
        public static readonly ComponentResourceKey DocumentWellTabButtonSelectedActivePressedBorder = new ComponentResourceKey(typeof(ResourceKeys), "DocumentWellTabButtonSelectedActivePressedBorder");
        public static readonly ComponentResourceKey DocumentWellTabButtonSelectedActivePressedGlyph = new ComponentResourceKey(typeof(ResourceKeys), "DocumentWellTabButtonSelectedActivePressedGlyph");
        public static readonly ComponentResourceKey DocumentWellTabButtonSelectedInactiveGlyph = new ComponentResourceKey(typeof(ResourceKeys), "DocumentWellTabButtonSelectedInactiveGlyph");
        public static readonly ComponentResourceKey DocumentWellTabButtonSelectedInactiveHoveredBackground = new ComponentResourceKey(typeof(ResourceKeys), "DocumentWellTabButtonSelectedInactiveHoveredBackground");
        public static readonly ComponentResourceKey DocumentWellTabButtonSelectedInactiveHoveredBorder = new ComponentResourceKey(typeof(ResourceKeys), "DocumentWellTabButtonSelectedInactiveHoveredBorder");
        public static readonly ComponentResourceKey DocumentWellTabButtonSelectedInactiveHoveredGlyph = new ComponentResourceKey(typeof(ResourceKeys), "DocumentWellTabButtonSelectedInactiveHoveredGlyph");
        public static readonly ComponentResourceKey DocumentWellTabButtonSelectedInactivePressedBackground = new ComponentResourceKey(typeof(ResourceKeys), "DocumentWellTabButtonSelectedInactivePressedBackground");
        public static readonly ComponentResourceKey DocumentWellTabButtonSelectedInactivePressedBorder = new ComponentResourceKey(typeof(ResourceKeys), "DocumentWellTabButtonSelectedInactivePressedBorder");
        public static readonly ComponentResourceKey DocumentWellTabButtonSelectedInactivePressedGlyph = new ComponentResourceKey(typeof(ResourceKeys), "DocumentWellTabButtonSelectedInactivePressedGlyph");
        public static readonly ComponentResourceKey DocumentWellTabButtonUnselectedTabHoveredGlyph = new ComponentResourceKey(typeof(ResourceKeys), "DocumentWellTabButtonUnselectedTabHoveredGlyph");
        // AccentColor
        public static readonly ComponentResourceKey DocumentWellTabButtonUnselectedTabHoveredButtonHoveredBackground = new ComponentResourceKey(typeof(ResourceKeys), "DocumentWellTabButtonUnselectedTabHoveredButtonHoveredBackground");
        // AccentColor
        public static readonly ComponentResourceKey DocumentWellTabButtonUnselectedTabHoveredButtonHoveredBorder = new ComponentResourceKey(typeof(ResourceKeys), "DocumentWellTabButtonUnselectedTabHoveredButtonHoveredBorder");
        public static readonly ComponentResourceKey DocumentWellTabButtonUnselectedTabHoveredButtonHoveredGlyph = new ComponentResourceKey(typeof(ResourceKeys), "DocumentWellTabButtonUnselectedTabHoveredButtonHoveredGlyph");
        // AccentColor
        public static readonly ComponentResourceKey DocumentWellTabButtonUnselectedTabHoveredButtonPressedBackground = new ComponentResourceKey(typeof(ResourceKeys), "DocumentWellTabButtonUnselectedTabHoveredButtonPressedBackground");
        // AccentColor
        public static readonly ComponentResourceKey DocumentWellTabButtonUnselectedTabHoveredButtonPressedBorder = new ComponentResourceKey(typeof(ResourceKeys), "DocumentWellTabButtonUnselectedTabHoveredButtonPressedBorder");
        public static readonly ComponentResourceKey DocumentWellTabButtonUnselectedTabHoveredButtonPressedGlyph = new ComponentResourceKey(typeof(ResourceKeys), "DocumentWellTabButtonUnselectedTabHoveredButtonPressedGlyph");

        // Tool Window : Caption
        // Background of selected toolwindow (AccentColor)
        public static readonly ComponentResourceKey ToolWindowCaptionActiveBackground = new ComponentResourceKey(typeof(ResourceKeys), "ToolWindowCaptionActiveBackground");
        public static readonly ComponentResourceKey ToolWindowCaptionActiveGrip = new ComponentResourceKey(typeof(ResourceKeys), "ToolWindowCaptionActiveGrip");
        public static readonly ComponentResourceKey ToolWindowCaptionActiveText = new ComponentResourceKey(typeof(ResourceKeys), "ToolWindowCaptionActiveText");
        public static readonly ComponentResourceKey ToolWindowCaptionInactiveBackground = new ComponentResourceKey(typeof(ResourceKeys), "ToolWindowCaptionInactiveBackground");
        public static readonly ComponentResourceKey ToolWindowCaptionInactiveGrip = new ComponentResourceKey(typeof(ResourceKeys), "ToolWindowCaptionInactiveGrip");
        public static readonly ComponentResourceKey ToolWindowCaptionInactiveText = new ComponentResourceKey(typeof(ResourceKeys), "ToolWindowCaptionInactiveText");

        // Tool Window : Caption : Button
        public static readonly ComponentResourceKey ToolWindowCaptionButtonActiveGlyph = new ComponentResourceKey(typeof(ResourceKeys), "ToolWindowCaptionButtonActiveGlyph");
        // AccentColor
        public static readonly ComponentResourceKey ToolWindowCaptionButtonActiveHoveredBackground = new ComponentResourceKey(typeof(ResourceKeys), "ToolWindowCaptionButtonActiveHoveredBackground");
        // AccentColor
        public static readonly ComponentResourceKey ToolWindowCaptionButtonActiveHoveredBorder = new ComponentResourceKey(typeof(ResourceKeys), "ToolWindowCaptionButtonActiveHoveredBorder");
        public static readonly ComponentResourceKey ToolWindowCaptionButtonActiveHoveredGlyph = new ComponentResourceKey(typeof(ResourceKeys), "ToolWindowCaptionButtonActiveHoveredGlyph");
        // AccentColor
        public static readonly ComponentResourceKey ToolWindowCaptionButtonActivePressedBackground = new ComponentResourceKey(typeof(ResourceKeys), "ToolWindowCaptionButtonActivePressedBackground");
        // AccentColor
        public static readonly ComponentResourceKey ToolWindowCaptionButtonActivePressedBorder = new ComponentResourceKey(typeof(ResourceKeys), "ToolWindowCaptionButtonActivePressedBorder");

        public static readonly ComponentResourceKey ToolWindowCaptionButtonActivePressedGlyph = new ComponentResourceKey(typeof(ResourceKeys), "ToolWindowCaptionButtonActivePressedGlyph");
        public static readonly ComponentResourceKey ToolWindowCaptionButtonInactiveGlyph = new ComponentResourceKey(typeof(ResourceKeys), "ToolWindowCaptionButtonInactiveGlyph");

        // AccentColor
        public static readonly ComponentResourceKey ToolWindowCaptionButtonInactiveHoveredBackground = new ComponentResourceKey(typeof(ResourceKeys), "ToolWindowCaptionButtonInactiveHoveredBackground");
        public static readonly ComponentResourceKey ToolWindowCaptionButtonInactiveHoveredBorder = new ComponentResourceKey(typeof(ResourceKeys), "ToolWindowCaptionButtonInactiveHoveredBorder");
        public static readonly ComponentResourceKey ToolWindowCaptionButtonInactiveHoveredGlyph = new ComponentResourceKey(typeof(ResourceKeys), "ToolWindowCaptionButtonInactiveHoveredGlyph");

        public static readonly ComponentResourceKey ToolWindowCaptionButtonInactivePressedBackground = new ComponentResourceKey(typeof(ResourceKeys), "ToolWindowCaptionButtonInactivePressedBackground");
        public static readonly ComponentResourceKey ToolWindowCaptionButtonInactivePressedBorder = new ComponentResourceKey(typeof(ResourceKeys), "ToolWindowCaptionButtonInactivePressedBorder");
        public static readonly ComponentResourceKey ToolWindowCaptionButtonInactivePressedGlyph = new ComponentResourceKey(typeof(ResourceKeys), "ToolWindowCaptionButtonInactivePressedGlyph");

        // Floating Document Window
        public static readonly ComponentResourceKey FloatingDocumentWindowBackground = new ComponentResourceKey(typeof(ResourceKeys), "FloatingDocumentWindowBackground");
        public static readonly ComponentResourceKey FloatingDocumentWindowBorder = new ComponentResourceKey(typeof(ResourceKeys), "FloatingDocumentWindowBorder");
        public static readonly ComponentResourceKey FloatingDocumentWindowBorderInactive = new ComponentResourceKey(typeof(ResourceKeys), "FloatingDocumentWindowBorderInactive");

        // Floating Tool Window
        public static readonly ComponentResourceKey FloatingToolWindowBackground = new ComponentResourceKey(typeof(ResourceKeys), "FloatingToolWindowBackground");
        public static readonly ComponentResourceKey FloatingToolWindowBorder = new ComponentResourceKey(typeof(ResourceKeys), "FloatingToolWindowBorder");
        public static readonly ComponentResourceKey FloatingToolWindowBorderInactive = new ComponentResourceKey(typeof(ResourceKeys), "FloatingToolWindowBorderInactive");

        // Navigator Window
        public static readonly ComponentResourceKey NavigatorWindowBackground = new ComponentResourceKey(typeof(ResourceKeys), "NavigatorWindowBackground");
        public static readonly ComponentResourceKey NavigatorWindowForeground = new ComponentResourceKey(typeof(ResourceKeys), "NavigatorWindowForeground");

        // Background of selected text in Navigator Window (AccentColor)
        public static readonly ComponentResourceKey NavigatorWindowSelectedBackground = new ComponentResourceKey(typeof(ResourceKeys), "NavigatorWindowSelectedBackground");
        public static readonly ComponentResourceKey NavigatorWindowSelectedText = new ComponentResourceKey(typeof(ResourceKeys), "NavigatorWindowSelectedText");
        public static readonly ComponentResourceKey NavigatorWindowUnSelectedBackground = new ComponentResourceKey(typeof(ResourceKeys), "NavigatorWindowUnSelectedBackground");

        #region DockingBrushKeys
        // Defines the height and width of the docking indicator buttons that are shown when
        // documents or tool windows are dragged
        public static readonly ComponentResourceKey DockingButtonWidthKey = new ComponentResourceKey(typeof(ResourceKeys), "DockingButtonWidthKey");
        public static readonly ComponentResourceKey DockingButtonHeightKey = new ComponentResourceKey(typeof(ResourceKeys), "DockingButtonHeightKey");

        public static readonly ComponentResourceKey DockingButtonBackgroundBrushKey = new ComponentResourceKey(typeof(ResourceKeys), "DockingButtonBackgroundBrushKey");
        public static readonly ComponentResourceKey DockingButtonForegroundBrushKey = new ComponentResourceKey(typeof(ResourceKeys), "DockingButtonForegroundBrushKey");
        public static readonly ComponentResourceKey DockingButtonForegroundArrowBrushKey = new ComponentResourceKey(typeof(ResourceKeys), "DockingButtonForegroundArrowBrushKey");

        public static readonly ComponentResourceKey DockingButtonStarBorderBrushKey = new ComponentResourceKey(typeof(ResourceKeys), "DockingButtonStarBorderBrushKey");
        public static readonly ComponentResourceKey DockingButtonStarBackgroundBrushKey = new ComponentResourceKey(typeof(ResourceKeys), "DockingButtonStarBackgroundBrushKey");

        // Preview Box is the highlighted rectangle that shows when a drop area in a window is indicated
        public static readonly ComponentResourceKey PreviewBoxBorderBrushKey = new ComponentResourceKey(typeof(ResourceKeys), "PreviewBoxBorderBrushKey");
        public static readonly ComponentResourceKey PreviewBoxBackgroundBrushKey = new ComponentResourceKey(typeof(ResourceKeys), "PreviewBoxBackgroundBrushKey");
        #endregion DockingBrushKeys

        /* By: Noisrev(Noisrev@outlook.com) --2022/5/28 */
        #region DockTargetButton BrushKeys

        public static readonly ComponentResourceKey DockTargetButtonBackgroundBrushKey = new ComponentResourceKey(typeof(ResourceKeys), "DockTargetButtonBackgroundBrushKey");
        public static readonly ComponentResourceKey DockTargetButtonBorderBrushKey = new ComponentResourceKey(typeof(ResourceKeys), "DockTargetButtonBorderBrushKey");
        public static readonly ComponentResourceKey DockTargetButtonGlyphBackgroundBrushKey = new ComponentResourceKey(typeof(ResourceKeys), "DockTargetButtonGlyphBackgroundBrushKey");
        public static readonly ComponentResourceKey DockTargetButtonGlyphBorderBrushKey = new ComponentResourceKey(typeof(ResourceKeys), "DockTargetButtonGlyphBorderBrushKey");
        public static readonly ComponentResourceKey DockTargetButtonGlyphArrowBrushKey = new ComponentResourceKey(typeof(ResourceKeys), "DockTargetButtonGlyphArrowBrushKey");
        public static readonly ComponentResourceKey DockTargetButtonOuterBackgroundBrushKey = new ComponentResourceKey(typeof(ResourceKeys), "DockTargetButtonOuterBackgroundBrushKey");
        public static readonly ComponentResourceKey DockTargetButtonOuterBorderBrushKey = new ComponentResourceKey(typeof(ResourceKeys), "DockTargetButtonOuterBorderBrushKey");

        #endregion DockTargetButton BrushKeys


        #region DocumentFloatingWindow

        public static readonly ComponentResourceKey DocumentFloatingWindowTitleBarBackground = new ComponentResourceKey(typeof(ResourceKeys), "DocumentFloatingWindowTitleBarBackground");
        public static readonly ComponentResourceKey DocumentFloatingWindowTitleBarText = new ComponentResourceKey(typeof(ResourceKeys), "DocumentFloatingWindowTitleBarText");
        public static readonly ComponentResourceKey DocumentFloatingWindowTitleBarTextActive = new ComponentResourceKey(typeof(ResourceKeys), "DocumentFloatingWindowTitleBarTextActive");
        public static readonly ComponentResourceKey DocumentFloatingWindowSystemButtonBackground = new ComponentResourceKey(typeof(ResourceKeys), "DocumentFloatingWindowSystemButtonBackground");
        public static readonly ComponentResourceKey DocumentFloatingWindowSystemButtonBackgroundHover = new ComponentResourceKey(typeof(ResourceKeys), "DocumentFloatingWindowSystemButtonBackgroundHover");
        public static readonly ComponentResourceKey DocumentFloatingWindowSystemButtonBackgroundPressed = new ComponentResourceKey(typeof(ResourceKeys), "DocumentFloatingWindowSystemButtonBackgroundPressed");
        public static readonly ComponentResourceKey DocumentFloatingWindowSystemButtonBorder = new ComponentResourceKey(typeof(ResourceKeys), "DocumentFloatingWindowSystemButtonBorder");
        public static readonly ComponentResourceKey DocumentFloatingWindowSystemButtonBorderHover = new ComponentResourceKey(typeof(ResourceKeys), "DocumentFloatingWindowSystemButtonBorderHover");
        public static readonly ComponentResourceKey DocumentFloatingWindowSystemButtonBorderPressed = new ComponentResourceKey(typeof(ResourceKeys), "DocumentFloatingWindowSystemButtonBorderPressed");
        public static readonly ComponentResourceKey DocumentFloatingWindowSystemButtonGlyph = new ComponentResourceKey(typeof(ResourceKeys), "DocumentFloatingWindowSystemButtonGlyph");
        public static readonly ComponentResourceKey DocumentFloatingWindowSystemButtonGlyphHover = new ComponentResourceKey(typeof(ResourceKeys), "DocumentFloatingWindowSystemButtonGlyphHover");
        public static readonly ComponentResourceKey DocumentFloatingWindowSystemButtonGlyphPressed = new ComponentResourceKey(typeof(ResourceKeys), "DocumentFloatingWindowSystemButtonGlyphPressed");

        #endregion DocumentFloatingWindow

        public static readonly ComponentResourceKey DocumentTabMenuItemGlyph = new ComponentResourceKey(typeof(ResourceKeys), "DocumentTabMenuItemGlyph");

        #region Visual Studio 2022 Style

        #region DocumentTab
        public static readonly ComponentResourceKey DocumentTabStateLineActive = new ComponentResourceKey(typeof(ResourceKeys), "DocumentTabStateLineActive");
        public static readonly ComponentResourceKey DocumentTabStateLineInactive = new ComponentResourceKey(typeof(ResourceKeys), "DocumentTabStateLineInactive");
        #endregion DocumentTab

        #region DocumentTabItem
        public static readonly ComponentResourceKey DocumentTabItemBorder = new ComponentResourceKey(typeof(ResourceKeys), "DocumentTabItemBorder");
        public static readonly ComponentResourceKey DocumentTabItemBorderActive = new ComponentResourceKey(typeof(ResourceKeys), "DocumentTabItemBorderActive");
        public static readonly ComponentResourceKey DocumentTabItemBorderActiveHover = new ComponentResourceKey(typeof(ResourceKeys), "DocumentTabItemBorderActiveHover");
        public static readonly ComponentResourceKey DocumentTabItemBorderInactive = new ComponentResourceKey(typeof(ResourceKeys), "DocumentTabItemBorderInactive");
        public static readonly ComponentResourceKey DocumentTabItemBorderUnselectedHover = new ComponentResourceKey(typeof(ResourceKeys), "DocumentTabItemBorderUnselectedHover");
        public static readonly ComponentResourceKey DocumentTabItemInBorder = new ComponentResourceKey(typeof(ResourceKeys), "DocumentTabItemInBorder");
        public static readonly ComponentResourceKey DocumentTabItemInBorderActive = new ComponentResourceKey(typeof(ResourceKeys), "DocumentTabItemInBorderActive");
        public static readonly ComponentResourceKey DocumentTabItemInBorderActiveHover = new ComponentResourceKey(typeof(ResourceKeys), "DocumentTabItemInBorderActiveHover");
        public static readonly ComponentResourceKey DocumentTabItemInBorderInactive = new ComponentResourceKey(typeof(ResourceKeys), "DocumentTabItemInBorderInactive");
        public static readonly ComponentResourceKey DocumentTabItemInBorderUnselectedHover = new ComponentResourceKey(typeof(ResourceKeys), "DocumentTabItemInBorderUnselectedHover");
        #endregion DocumentTabItem

        #region ToolWell
        public static readonly ComponentResourceKey ToolWellInnerBorder = new ComponentResourceKey(typeof(ResourceKeys), "ToolWellInnerBorder");
        public static readonly ComponentResourceKey ToolWellInnerBorderInactive = new ComponentResourceKey(typeof(ResourceKeys), "ToolWellInnerBorderInactive");
        #endregion ToolWellInnerBorder

        #endregion Visual Studio 2022 Style

        #region ContextMenu

        public static readonly ComponentResourceKey ContextMenuBackground = new ComponentResourceKey(typeof(ResourceKeys), "ContextMenuBackground");
        public static readonly ComponentResourceKey ContextMenuBorderBrush = new ComponentResourceKey(typeof(ResourceKeys), "ContextMenuBorderBrush");
        public static readonly ComponentResourceKey ContextMenuRectangleFill = new ComponentResourceKey(typeof(ResourceKeys), "ContextMenuRectangleFill");

        #endregion ContextMenu

        #region TabItem

        public static readonly ComponentResourceKey TabItemForeground = new ComponentResourceKey(typeof(ResourceKeys), "TabItemForeground");
        public static readonly ComponentResourceKey TabItemStaticBackground = new ComponentResourceKey(typeof(ResourceKeys), "TabItemStaticBackground");
        public static readonly ComponentResourceKey TabItemStaticBorder = new ComponentResourceKey(typeof(ResourceKeys), "TabItemStaticBorder");
        public static readonly ComponentResourceKey TabItemStaticInBorder = new ComponentResourceKey(typeof(ResourceKeys), "TabItemStaticInBorder");
        public static readonly ComponentResourceKey TabItemStaticForeground = new ComponentResourceKey(typeof(ResourceKeys), "TabItemStaticForeground");
        public static readonly ComponentResourceKey TabItemMouseOverBackground = new ComponentResourceKey(typeof(ResourceKeys), "TabItemMouseOverBackground");
        public static readonly ComponentResourceKey TabItemMouseOverBorder = new ComponentResourceKey(typeof(ResourceKeys), "TabItemMouseOverBorder");
        public static readonly ComponentResourceKey TabItemMouseOverInBorder = new ComponentResourceKey(typeof(ResourceKeys), "TabItemMouseOverInBorder");
        public static readonly ComponentResourceKey TabItemMouseOverForeground = new ComponentResourceKey(typeof(ResourceKeys), "TabItemMouseOverForeground");
        public static readonly ComponentResourceKey TabItemDisabledBackground = new ComponentResourceKey(typeof(ResourceKeys), "TabItemDisabledBackground");
        public static readonly ComponentResourceKey TabItemDisabledBorder = new ComponentResourceKey(typeof(ResourceKeys), "TabItemDisabledBorder");
        public static readonly ComponentResourceKey TabItemDisabledInBorder = new ComponentResourceKey(typeof(ResourceKeys), "TabItemDisabledInBorder");
        public static readonly ComponentResourceKey TabItemDisabledForeground = new ComponentResourceKey(typeof(ResourceKeys), "TabItemDisabledForeground");
        public static readonly ComponentResourceKey TabItemSelectedBackground = new ComponentResourceKey(typeof(ResourceKeys), "TabItemSelectedBackground");
        public static readonly ComponentResourceKey TabItemSelectedBorder = new ComponentResourceKey(typeof(ResourceKeys), "TabItemSelectedBorder");
        public static readonly ComponentResourceKey TabItemSelectedInBorder = new ComponentResourceKey(typeof(ResourceKeys), "TabItemSelectedInBorder");
        public static readonly ComponentResourceKey TabItemSelectedForeground = new ComponentResourceKey(typeof(ResourceKeys), "TabItemSelectedForeground");

        #endregion TabItem

        #region MenuItem

        public static readonly ComponentResourceKey MenuItemBackground = new ComponentResourceKey(typeof(ResourceKeys), "MenuItemBackground");
        public static readonly ComponentResourceKey MenuItemBackgroundHover = new ComponentResourceKey(typeof(ResourceKeys), "MenuItemBackgroundHover");
        public static readonly ComponentResourceKey MenuItemBackgroundDisabled = new ComponentResourceKey(typeof(ResourceKeys), "MenuItemBackgroundDisabled");
        public static readonly ComponentResourceKey MenuItemBorder = new ComponentResourceKey(typeof(ResourceKeys), "MenuItemBorder");
        public static readonly ComponentResourceKey MenuItemBorderHover = new ComponentResourceKey(typeof(ResourceKeys), "MenuItemBorderHover");
        public static readonly ComponentResourceKey MenuItemBorderDisabled = new ComponentResourceKey(typeof(ResourceKeys), "MenuItemBorderDisabled");
        public static readonly ComponentResourceKey MenuItemForeground = new ComponentResourceKey(typeof(ResourceKeys), "MenuItemForeground");
        public static readonly ComponentResourceKey MenuItemForegroundHover = new ComponentResourceKey(typeof(ResourceKeys), "MenuItemForegroundHover");
        public static readonly ComponentResourceKey MenuItemForegroundDisabled = new ComponentResourceKey(typeof(ResourceKeys), "MenuItemForegroundDisabled");
        public static readonly ComponentResourceKey MenuItemGlyph = new ComponentResourceKey(typeof(ResourceKeys), "MenuItemGlyph");
        public static readonly ComponentResourceKey MenuItemGlyphHover = new ComponentResourceKey(typeof(ResourceKeys), "MenuItemGlyphHover");
        public static readonly ComponentResourceKey MenuItemGlyphDisabled = new ComponentResourceKey(typeof(ResourceKeys), "MenuItemGlyphDisabled");
        public static readonly ComponentResourceKey MenuItemGlyphPanel = new ComponentResourceKey(typeof(ResourceKeys), "MenuItemGlyphPanel");
        public static readonly ComponentResourceKey MenuItemGlyphPanelHover = new ComponentResourceKey(typeof(ResourceKeys), "MenuItemGlyphPanelHover");
        public static readonly ComponentResourceKey MenuItemGlyphPanelDisabled = new ComponentResourceKey(typeof(ResourceKeys), "MenuItemGlyphPanelDisabled");
        public static readonly ComponentResourceKey MenuItemGlyphPanelBorder = new ComponentResourceKey(typeof(ResourceKeys), "MenuItemGlyphPanelBorder");
        public static readonly ComponentResourceKey MenuItemGlyphPanelBorderHover = new ComponentResourceKey(typeof(ResourceKeys), "MenuItemGlyphPanelBorderHover");
        public static readonly ComponentResourceKey MenuItemGlyphPanelBorderDisabled = new ComponentResourceKey(typeof(ResourceKeys), "MenuItemGlyphPanelBorderDisabled");

        #endregion MenuItem

        public static readonly ComponentResourceKey SystemColorsMenuText = new ComponentResourceKey(typeof(ResourceKeys), "SystemColorsMenuText");

        #endregion Brush Keys
    }
}
