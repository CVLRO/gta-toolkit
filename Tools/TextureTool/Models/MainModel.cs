﻿/*
    Copyright(c) 2015 Neodymium

    Permission is hereby granted, free of charge, to any person obtaining a copy
    of this software and associated documentation files (the "Software"), to deal
    in the Software without restriction, including without limitation the rights
    to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
    copies of the Software, and to permit persons to whom the Software is
    furnished to do so, subject to the following conditions:

    The above copyright notice and this permission notice shall be included in
    all copies or substantial portions of the Software.

    THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
    IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
    FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
    AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
    LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
    OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
    THE SOFTWARE.
*/

using RageLib.Helpers;
using RageLib.ResourceWrappers;
using RageLib.ResourceWrappers.GTA5.PC.Textures;
using System;
using System.IO;

namespace TextureTool.Models
{
    public class MainModel
    {
        private ITextureDictionaryFile textureDictionaryFile;

        public ITextureList TextureList
        {
            get
            {
                if (textureDictionaryFile != null)
                    return textureDictionaryFile.TextureDictionary.Textures;
                else
                    return null;
            }
        }

        public string FileName { get; set; }

        public void New()
        {
            textureDictionaryFile = new TextureDictionaryFileWrapper_GTA5_pc();
            FileName = null;
        }

        public void Load(string fileName)
        {
            textureDictionaryFile = new TextureDictionaryFileWrapper_GTA5_pc();
            textureDictionaryFile.Load(fileName);
            FileName = fileName;
        }

        public void Save(string fileName)
        {
            textureDictionaryFile.Save(fileName);
            FileName = fileName;
        }

        public void Import(string fileName)
        {
            try
            {
                var info = new FileInfo(fileName);

                // remove texture from list
                for (int i = TextureList.Count - 1; i >= 0; i--)
                    if (TextureList[i].Name.Equals(info.Name.Replace(".dds", ""), StringComparison.OrdinalIgnoreCase))
                        TextureList.RemoveAt(i);

                ITexture texture = new TextureWrapper_GTA5_pc();
                texture.Name = info.Name.Replace(".dds", "");
                DDSIO.LoadTextureData(texture, fileName);

                // add texture to list
                TextureList.Add(texture);
            }
            catch
            { }
        }

        public void Export(ITexture texture, string fileName)
        {
            try
            {
                DDSIO.SaveTextureData(texture, fileName);
            }
            catch
            { }
        }

        public void Delete(ITexture texture)
        {
            TextureList.Remove(texture);
        }
    }
}