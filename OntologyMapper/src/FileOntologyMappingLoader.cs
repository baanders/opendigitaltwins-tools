﻿// -----------------------------------------------------------------------
// <copyright file="FileOntologyMappingLoader.cs" company="Microsoft">
// Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.SmartFacilities.OntologyMapper
{
    using Microsoft.Extensions.Logging;
    using Newtonsoft.Json;

    public class FileOntologyMappingLoader : IOntologyMappingLoader
    {
        private readonly ILogger logger;
        private readonly string filePath = string.Empty;

        public FileOntologyMappingLoader(ILogger logger, string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
            {
                throw new ArgumentNullException(nameof(filePath));
            }

            this.logger = logger;
            this.filePath = filePath;
        }

        public OntologyMapping LoadOntologyMapping()
        {
            logger.LogInformation("Loading Ontology Mapping file: {fileName}", filePath);

            var file = File.ReadAllText(filePath);

            var mappings = JsonConvert.DeserializeObject<OntologyMapping>(file);
            if (mappings != null)
            {
                return mappings;
            }
            else
            {
                throw new MappingFileException("Mappings file is empty or poorly formed.", filePath);
            }
        }
    }
}
