using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace Jeu_de_combat
{
    public class Animation
    {
        internal List<byte> frms { get; set; }
        internal float dt { get; set; }
        internal bool loop { get; set; }
        internal bool persist { get; set; }
        internal bool fnshed { get; set; }

        internal Animation(List<byte> pFrms, float pDt, bool pLoop, bool pPersist)
        {
            frms = pFrms;
            dt = pDt;
            loop = pLoop;
            if (pLoop)
            {
                persist = false;
            }
            else
            {
                persist = pPersist;
            }
            fnshed = false;
        }
    }
}
