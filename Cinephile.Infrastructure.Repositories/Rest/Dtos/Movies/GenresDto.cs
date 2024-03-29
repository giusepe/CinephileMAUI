﻿// Copyright (c) 2019 .NET Foundation and Contributors. All rights reserved.
// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Cinephile.Infrastructure.Repositories.Rest.Dtos.Movies
{
    /// <summary>
    /// Contains the different available genres.
    /// </summary>
    public class GenresDto
    {
        /// <summary>
        /// Gets or sets the genres.
        /// </summary>
        [SuppressMessage("Design", "CA2227: Change to be read-only by removing the property setter.", Justification = "Used in DTO object.")]
        public IList<GenreDto> Genres { get; set; }
    }
}
