﻿// Copyright (c) Lucas Girouard-Stranks. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

internal static class Program
{
    private static void Main(string[] args)
    {
        using var app = new NonInterleavedApplication();
        app.Run();
    }
}