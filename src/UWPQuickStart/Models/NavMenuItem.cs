// Copyright (c) Microsoft. All rights reserved
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using Windows.UI.Xaml.Controls;

namespace UWPQuickStart
{
    //Data to represent an item in the nav menu (also known as hamburger menu).
    public class NavMenuItem
    {
        public string Label { get; set; }
        public Symbol Symbol { get; set; }

        public char SymbolAsChar => (char) Symbol;

        public Type DestPage { get; set; }
        public object Arguments { get; set; }
    }
}