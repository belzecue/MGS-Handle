/*************************************************************************
 *  Copyright © 2018 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  ILED.cs
 *  Description  :  Interface for LED.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  9/22/2018
 *  Description  :  Initial development version.
 *************************************************************************/

namespace Mogoson.Device
{
    /// <summary>
    /// Interface for LED.
    /// </summary>
    public interface ILED : IDevice
    {
        #region Method
        /// <summary>
        /// Open LED.
        /// </summary>
        void Open();

        /// <summary>
        /// Close LED.
        /// </summary>
        void Close();
        #endregion
    }
}