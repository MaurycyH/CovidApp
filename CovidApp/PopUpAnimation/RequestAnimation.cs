using Rg.Plugins.Popup.Animations.Base;
using Rg.Plugins.Popup.Pages;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CovidApp.PopUpAnimation
{
    public class RequestAnimation : FadeBackgroundAnimation
    {
        private double mDefaultTranslationY;
        public RequestAnimation()
        {
            DurationIn = DurationOut = 300;
            EasingIn = Easing.CubicOut;
            EasingOut = Easing.CubicIn;
        }
        public override void Preparing(View content, PopupPage page)
        {
            base.Preparing(content, page);

            HidePage(page);

            if (content == null) return;

            mDefaultTranslationY = content.TranslationY;
        }
        public override void Disposing(View content, PopupPage page)
        {
            base.Disposing(content, page);

            ShowPage(page);

            if (content == null) return;

            content.TranslationY = mDefaultTranslationY;
        }

        public async override Task Appearing(View content, PopupPage page)
        {
            var taskList = new List<Task>();

            taskList.Add(base.Appearing(content, page));

            if (content != null)
            {
                var topOffset = GetTopOffset(content, page);
                content.TranslationY = topOffset;

                taskList.Add(content.TranslateTo(content.TranslationX, mDefaultTranslationY, DurationIn, EasingIn));
            };

            ShowPage(page);

            await Task.WhenAll(taskList);
        }

        public async override Task Disappearing(View content, PopupPage page)
        {
            var taskList = new List<Task>();

            taskList.Add(base.Disappearing(content, page));

            if (content != null)
            {
                mDefaultTranslationY = content.TranslationX;

                var topOffset = GetTopOffset(content, page);

                taskList.Add(content.TranslateTo(content.TranslationX, topOffset, DurationOut, EasingOut));
            };

            await Task.WhenAll(taskList);
        }
    }
}
