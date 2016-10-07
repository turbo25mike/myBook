using Autofac;
using System;
using System.Collections.Generic;
using System.Text;

namespace DrawIt
{
    public class AppModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<StoryManagerViewModel>().SingleInstance();
            builder.RegisterType<StoryManagerView>().SingleInstance();
        }
    }
}
