using System;
using Newtonsoft.Json;

namespace Kralizek.Assembla.Connector.Spaces
{
    public class Space
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("wiki_name")]
        public string WikiName { get; set; }

        [JsonProperty("public_permissions")]
        public PublicPermissions PublicPermissions { get; set; }

        [JsonProperty("team_permissions")]
        public TeamPermissions TeamPermissions { get; set; }

        [JsonProperty("watcher_permissions")]
        public WatcherPermissions WatcherPermissions { get; set; }

        [JsonProperty("share_permissions")]
        public bool SharePermissions { get; set; }

        [JsonProperty("team_tab_role")]
        public TeamTabRoles TeamTabRole { get; set; }

        [JsonProperty("created_at")]
        public DateTimeOffset CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        public DateTimeOffset UpdatedAt { get; set; }

        [JsonProperty("deafult_showpage")]
        public string DefaultShowPage { get; set; }

        [JsonProperty("tabs_order")]
        [JsonConverter(typeof(TabListJsonConverter))]
        public string[] TabsOrder { get; set; }

        [JsonProperty("parent_id")]
        public string ParentId { get; set; }

        [JsonProperty("restricted")]
        public bool IsRestricted { get; set; }

        [JsonProperty("restricted_date")]
        public DateTimeOffset? RestrictedAt { get; set; }

        [JsonProperty("commercial_from")]
        public DateTimeOffset? CommercialFrom { get; set; }

        [JsonProperty("banner")]
        public string Banner { get; set; }

        [JsonProperty("banner_height")]
        public int? BannerHeight { get; set; }

        [JsonProperty("banner_text")]
        public string BannerText { get; set; }

        [JsonProperty("banner_link")]
        public string BannerLink { get; set; }

        [JsonProperty("style")]
        public string CssStyle { get; set; }

        [JsonProperty("status")]
        public SpaceStatus Status { get; set; }

        [JsonProperty("approved")]
        public bool Approved { get; set; }

        [JsonProperty("is_manager")]
        public bool IsManager { get; set; }

        [JsonProperty("is_volunteer")]
        public bool IsVolunteer { get; set; }

        [JsonProperty("is_commercial")]
        public bool IsCommercial { get; set; }

        [JsonProperty("can_join")]
        public bool CanJoin { get; set; }

        [JsonProperty("last_payer_changed_at")]
        public DateTimeOffset? LastPayerChangedAt { get; set; }
    }

    public enum PublicPermissions
    {
        Private = 0,
        Public = 1
    }

    public enum TeamPermissions
    {
        ViewOnly = 1,
        ViewAndEdit = 2,
        AllPermissions = 3
    }

    public enum WatcherPermissions
    {
        CanAccess = 1
    }

    public enum TeamTabRoles
    {
        AvailableToAnyone = 0,
        AvialableToWatchers = 10,
        AvailableToUsers = 50,
        AvailableToOwners = 90
    }

    public enum SpaceStatus
    {
        Proposed = 0,
        Active = 1,
        Archived = 2,
        Proposed2 = 3,
        Shared = 4
    }
}