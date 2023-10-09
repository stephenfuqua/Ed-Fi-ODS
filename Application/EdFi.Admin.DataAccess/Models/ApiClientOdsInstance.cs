﻿// SPDX-License-Identifier: Apache-2.0
// Licensed to the Ed-Fi Alliance under one or more agreements.
// The Ed-Fi Alliance licenses this file to you under the Apache License, Version 2.0.
// See the LICENSE and NOTICES files in the project root for more information.

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EdFi.Admin.DataAccess.Models
{
    /// <summary>
    /// Class representing the association of an Api Client with an Ods Instance
    /// </summary>
    [Index("ApiClientId")]
    [Index("OdsInstanceId")]
    public class ApiClientOdsInstance
    {
        /// <summary>
        /// Numeric Identifier which is an Identity column which distinguish the unique combination of ApiClient and OdsInstance
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ApiClientOdsInstanceId { get; set; }
        
        public virtual ApiClient ApiClient { get; set; }
        
        public virtual OdsInstance OdsInstance { get; set; }
    }
}
