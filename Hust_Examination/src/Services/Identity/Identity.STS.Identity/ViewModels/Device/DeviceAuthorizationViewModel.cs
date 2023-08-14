// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

// Original file: httpss://github.com/IdentityServer/IdentityServer4.Quickstart.UI
// Modified by Jan �koruba

using Identity.STS.Identity.ViewModels.Consent;

namespace Identity.STS.Identity.ViewModels.Device
{
    public class DeviceAuthorizationViewModel : ConsentViewModel
    {
        public string UserCode { get; set; }
        public bool ConfirmUserCode { get; set; }
    }
}