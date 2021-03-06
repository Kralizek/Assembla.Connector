﻿using System;
using Newtonsoft.Json;

namespace Kralizek.Assembla.Connector.Spaces.Tools
{
    public class Tool
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("space_id")]
        public string SpaceId { get; set; }

        [JsonProperty("active")]
        public bool IsActive { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("number")]
        public int Number { get; set; }

        [JsonProperty("watcher_permissions")]
        public Permissions? WatcherPermissions { get; set; }

        [JsonProperty("team_permissions")]
        public Permissions? TeamPermissions { get; set; }

        [JsonProperty("public_permissions")]
        public Permissions? PublicPermissions { get; set; }

        [JsonProperty("created_at")]
        public DateTimeOffset CreatedAt { get; set; }

        [JsonProperty("tool_id")]
        public ToolType ToolType { get; set; }

        [JsonProperty("parent_id")]
        public string Parent { get; set; }

        [JsonProperty("menu_name")]
        public string MenuName { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}