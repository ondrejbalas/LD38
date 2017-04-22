﻿using Duality;
using Duality.Components;
using Duality.Components.Renderers;
using Duality.Drawing;

namespace PuddleJumper.Core.GameObjects
{
    [RequiredComponent(typeof(TextRenderer))]
    [RequiredComponent(typeof(Transform))]
    public class Airport : Component, ICmpInitializable
    {
        public string Name { get; set; }
        public int Size { get; set; }

        public Airport(string name)
        {
            Name = name;
            Size = 1;
        }

        public void OnInit(InitContext context)
        {
            var renderer = GameObj.GetComponent<TextRenderer>();
            renderer.Text = new FormattedText($"{Name} - {Size}");
        }

        public void OnShutdown(ShutdownContext context)
        {
        }
    }
}