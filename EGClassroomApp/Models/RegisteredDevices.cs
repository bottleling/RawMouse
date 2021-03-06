﻿using EGClassroom.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace EGClassroom.Models
{
    public class RegisteredDevices
    {
        public static IEnumerable<RegisteredDevice> LoadDevices()
        {
            List<RegisteredDevice>  presets = new List<RegisteredDevice>();
            String filePath = Path.Combine(
                                Directory.GetParent( 
                                                        Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location)
                                                    ).Parent.FullName,  @"Resources\PresetDevices.xml");
            System.Diagnostics.Debug.Print(filePath);
            if (System.IO.File.Exists(filePath))
            {
                XDocument objDoc = XDocument.Load(filePath);
                foreach (var device in objDoc.Descendants("device"))
                {
                    string deviceId = device.Element("DeviceID").Value;
                    string studentName = device.Element("StudentName").Value;
                    string imagePath = device.Element("ImagePath").Value;
                    Helper.RoleEnum role = Helper.RoleEnum.STUDENT;
                    if (device.Element("Role").Value.ToUpper() == "TEACHER") role = Helper.RoleEnum.TEACHER;                   
                    presets.Add(new RegisteredDevice() { DeviceID = deviceId, Name = studentName, ImagePath = imagePath, Role = role});

                }
            }
            return presets;
        }
    }
}
