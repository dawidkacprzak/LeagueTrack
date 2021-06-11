/*
* Copyright (C) 2021 Dawid Kacprzak, Oyacode
* License: https://www.gnu.org/licenses/gpl-3.0.html GPL version 3
* Author: Dawid Kacprzak https://github.com/dawidkacprzak 
*/
using System;
using ApiWrapper.Abstract.Request;
using ApiWrapper.Enum;

namespace ApiWrapper.Implementation.Request
{
    /// <summary>
    /// IRequest wrapper. Includes all needed fields for riot API.
    /// </summary>
    public class RiotRequest : ARequestBase
    {
        /// <summary>
        /// Final request uri - exact value is used for requesting riot api
        /// </summary>
        protected override string httpAddress
        {
            get => $"https://{location.ToTextName()}.api.riotgames.com{this.MethodPath}"; 
            set => throw new Exception("Cannot override RiotRequest address. It is building automatically.");
        }

        private ELocation location;
        
        /// <summary>
        /// Set server location. Used for Uri generating
        /// </summary>
        /// <param name="location"></param>
        public void SetLocation(ELocation location)
        {
            this.location = location;
        }
    }
}