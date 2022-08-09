#region Copyright Notice
// ----------------------------------------------------------------------------
// <copyright file="CellularNetworkConfiguration.cs" company="Flowbird Transport Ltd">
//  This document contains copyright material, trade names and marks and other 
//  proprietary information, including, but not limited to, text, software, 
//  photos and graphics, and may in future include video, graphics, music and 
//  sound ("Content"). The Content is protected by copyright and patent laws 
//  and legislation, registered and unregistered trademarks, database rights 
//  and other intellectual property rights.
//
//  Flowbird Transport Ltd, its licensors, or authorised contributors own the 
//  copyright, database rights and other intellectual property rights in the 
//  selection, coordination, arrangement and enhancement of such Content, as 
//  well as in the Content original to it.
//
//  You may not modify, publish, transmit, participate in the transfer or sale
//  of, create derivative works from, or in any way exploit any of the Content,
//  in whole or in part, except as provided in these terms of use.
//
//  You may use the Content for your own business use as directed and agreed in 
//  writing by Flowbird Transport Ltd only. Except as otherwise expressly 
//  permitted under copyright law, no copying, redistribution, retransmission, 
//  publication or commercial exploitation of the Content will be permitted 
//  without Flowbird Transport Ltd's express permission.
//
//  In the event of any permitted copying, redistribution or publication of 
//  copyright material, no changes in or deletion of author attribution, 
//  trademark legend or copyright notice shall be made. You acknowledge that 
//  you do not acquire any ownership rights by copying or using the copyright 
//  material.
// </copyright>
// ----------------------------------------------------------------------------
#endregion

namespace CellWiFiInterfacePlugin.Android
{
    public class CellularNetworkConfiguration
    {
        public string Apn { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}