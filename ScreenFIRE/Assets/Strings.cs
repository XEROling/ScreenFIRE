using ScreenFIRE.Modules.Companion;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ScreenFIRE.Assets {

    internal enum IStrings {
        ScreenFIRE_Stylized,
        ScreenFIRE,
        ScreenFIREConfig,
        ScreenFIRERepositoryAtGitHub,

        GNUGeneralPublicLicensev3_0___,

        OK, Yes, No, Cancel,

        Screenshot,
        SavingOptions,
        About,

        Version, Phase, Public, Beta, Development,
        MadeWith_NET_GTK_,

        ChooseHowYouWouldLikeToFireYourScreenshot_,
        FiredAScreenshot_,
        ThisButtonHasBeenClicked,
        times_1,
        times_2,

        SomethingWentWrong___,

        ViewScreenshots,
        AllMonitors,
        MonitorAtPointer,
        WindowAtPointer,
        ActiveWindow,
        FreeAreaSelection,

        SaveAs___,
        FileAlreadyExists_,
        alreadyExists,
        WouldYouLikeToReplaceTheExistingFile_,

        AutoDelete1MonthOldFiles,
        Original,
        Quality,
        Efficiency,
        Animated,
        Video,

        ClicktoCopy___,

        FillColor,
        BorderColor,
    }

    internal static partial class Strings {
        public static IStrings ToIStrings(this string value)
            => (IStrings)Enum.Parse(typeof(IStrings), value, true);


        internal static async Task<string> FetchAndJoin(params IStrings[] Names) {
            return string.Join(" ", await Fetch(Names));
        }

        /// <summary> Fetch a set of strings </summary>
        /// <param name="Names"> String names provided by <see cref="IStrings"/> </param>
        /// <returns> Localized <see cref="string"/>[] array according to system language </returns>
        internal static async Task<string[]> Fetch(params IStrings[] Names) {
            List<string> result = new();
            foreach (var name in Names)
                result.Add(await Fetch(name,
                                       false)); //? skip translation when fetching multiple strings


            //? >> Translation is currently disabled || `Languages.TranslateText` is broken


            //!? Remove if enabling translation >>
            return result.ToArray();

            //!? Keep >>

            string joint = "\u21da\u21db"; //? ⇚⇛ Where strings will be joined/split

            string joined = string.Join(joint, result);

            string joined_Translated
                = await Languages.TranslateText(joined, Languages.SystemLanguage()); //? translate all at once

            string[] result_Translated = joined_Translated.Split(joint);

            return result_Translated;
        }

        /// <summary> Fetch a specific string </summary>
        /// <param name="stringName"> String name provided by <see cref="IStrings"/> </param>
        /// <returns> Localized <see cref="string"/> according to system language </returns>
        internal static async Task<string> Fetch(IStrings stringName, bool translate = true, ILanguages language = ILanguages.System) {
            string result;
            Dictionary<IStrings, string> language_stringsStore;

            Common.Cache.StringsStore ??= new();

            //? Get exising string if already stored
            if (Common.Cache.StringsStore.TryGetValue(language, out language_stringsStore)) {
                if (language_stringsStore.TryGetValue(stringName, out result))
                    return result;
            }
            //? Create if still null
            language_stringsStore ??= new();

            //? Fetch
            result = ((language == ILanguages.System)
                      ? Languages.SystemLanguage() : language)
                      switch {
                          //ILanguages.English => En(Name),
                          ILanguages.Arabic => Ar(stringName),
                          ILanguages.ChineseSimplified => Zh(stringName),

                          //? English / Other
                          _ => translate ? await Languages.TranslateText(En(stringName), language) : En(stringName),
                      };

            //? Store it for later use
            language_stringsStore.Add(stringName, result);
            try { Common.Cache.StringsStore.Remove(language); } catch { } //? Ignore if failed
            Common.Cache.StringsStore.Add(language, language_stringsStore);
            Common.Cache.Save();

            return result;
        }


        #region Localized strings

        private static string En(IStrings Name)
            => Name switch {
                IStrings.ScreenFIRE_Stylized => @"Screen 🅵🅸🆁🅴",
                IStrings.ScreenFIRE => "ScreenFIRE",
                IStrings.ScreenFIREConfig => "ScreenFIRE Configuration",
                IStrings.ScreenFIRERepositoryAtGitHub => $"ScreenFIRE repository at GitHub",

                IStrings.GNUGeneralPublicLicensev3_0___ => $"GNU General Public License v3.0 {Common.Ellipses}",

                IStrings.Screenshot => "Screenshot",
                IStrings.SavingOptions => "Saving options",
                IStrings.About => "About",

                IStrings.Version => "Version",
                IStrings.Phase => "Phase",
                IStrings.Public => "Public",
                IStrings.Beta => "Beta",
                IStrings.Development => "Development",
                IStrings.MadeWith_NET_GTK_ => "Made with .NET & GTK#",

                IStrings.OK => "OK",
                IStrings.Yes => "Yes",
                IStrings.No => "No",
                IStrings.Cancel => "Cancel",

                IStrings.ChooseHowYouWouldLikeToFireYourScreenshot_ => "Choose how you would like to to fire your screenshot!",
                IStrings.FiredAScreenshot_ => "Fired a screenshot!",
                IStrings.ThisButtonHasBeenClicked => "This button has been clicked",
                IStrings.times_1 => "time",
                IStrings.times_2 => "times",

                IStrings.SomethingWentWrong___ => $"Something went wrong{Common.Ellipses}",

                IStrings.ViewScreenshots => $"View screenshots{Common.Ellipses}",
                IStrings.AllMonitors => "All monitors",
                IStrings.MonitorAtPointer => "Monitor at pointer",
                IStrings.WindowAtPointer => "Window at pointer",
                IStrings.ActiveWindow => "Active window",
                IStrings.FreeAreaSelection => "Free area selection",

                IStrings.SaveAs___ => $"Save as{Common.Ellipses}",
                IStrings.FileAlreadyExists_ => "File already exists.",
                IStrings.alreadyExists => "already exists",
                IStrings.WouldYouLikeToReplaceTheExistingFile_ => "Would you like to replace the existing file?",

                IStrings.AutoDelete1MonthOldFiles => "Auto delete 1 month old files",
                IStrings.Original => "Original",
                IStrings.Quality => "Quality",
                IStrings.Efficiency => "Efficiency",
                IStrings.Animated => "Animated",
                IStrings.Video => "Video",

                IStrings.ClicktoCopy___ => $"Click to copy{Common.Ellipses}",

                IStrings.FillColor => "Fill Color",
                IStrings.BorderColor => "Border Color",

                //!? Last resort
                _ => $"⚠ STRING MISSING: \"{Name}\" ⚠"
            };

        private static string Ar(IStrings Name)
            => Name switch {
                IStrings.ScreenFIRE_Stylized => @"Screen 🅵🅸🆁🅴",
                IStrings.ScreenFIRE => "(حريق الشاشة) ScreenFIRE",
                IStrings.ScreenFIREConfig => "إعدادات ScreenFIRE",
                IStrings.ScreenFIRERepositoryAtGitHub => $"مستودع ScreenFIRE على GitHub",

                IStrings.GNUGeneralPublicLicensev3_0___ => $"رخصة GNU العامة v3.0 {Common.Ellipses}",

                IStrings.Screenshot => "لقطة شاشة",
                IStrings.SavingOptions => "خيارات الحفظ",
                IStrings.About => "حول هذا",

                IStrings.Version => "الإصدار",
                IStrings.Phase => "المرحلة",
                IStrings.Public => "عام",
                IStrings.Beta => "بيتا",
                IStrings.Development => "تطوير",
                IStrings.MadeWith_NET_GTK_ => "تم إنشاؤه باستخدام .NET و GTK#",

                IStrings.OK => "حسناً",
                IStrings.Yes => "نعم",
                IStrings.No => "لا",
                IStrings.Cancel => "إلغاء",

                IStrings.ChooseHowYouWouldLikeToFireYourScreenshot_ => "اختر كيف تود طلق صورة شاشتك!",
                IStrings.FiredAScreenshot_ => "تم اطلاق صورة الشاشة!",
                IStrings.ThisButtonHasBeenClicked => "هذا الزر قد ضغط",
                IStrings.times_1 => "مرة",
                IStrings.times_2 => "مرات",

                IStrings.SomethingWentWrong___ => $"حدث خطأ ما{Common.Ellipses}",

                IStrings.ViewScreenshots => $"عرض القطات{Common.Ellipses}",
                IStrings.AllMonitors => "جميع الشاشات",
                IStrings.MonitorAtPointer => "الشاشة عند المؤشر",
                IStrings.WindowAtPointer => "النافذة عند المؤشر",
                IStrings.ActiveWindow => "النافذة الفعالة",
                IStrings.FreeAreaSelection => "تحديد مساحة حرة",

                IStrings.SaveAs___ => $"حفظ باسم{Common.Ellipses}",
                IStrings.FileAlreadyExists_ => "الملف موجود مسبقاً.",
                IStrings.alreadyExists => "موجود مسبقاً",
                IStrings.WouldYouLikeToReplaceTheExistingFile_ => "هل تودّ استبدال الملف السابق؟",

                IStrings.AutoDelete1MonthOldFiles => "حذف تلقائي للملفات التي مضى عليها شهر",
                IStrings.Original => "الأصل",
                IStrings.Quality => "جودة",
                IStrings.Efficiency => "كفاءة",
                IStrings.Animated => "متحركة",
                IStrings.Video => "فيديو",

                IStrings.ClicktoCopy___ => $"انقر للنسخ{Common.Ellipses}",

                IStrings.FillColor => "لون التعبئة",
                IStrings.BorderColor => "لون الحدود",


                //! Fallback to English.
                _ => En(Name)
            };

        private static string Zh(IStrings Name)
            => Name switch {
                IStrings.ScreenFIRE_Stylized => @"Screen 🅵🅸🆁🅴",
                IStrings.ScreenFIRE => "（屏幕火） ScreenFIRE",
                IStrings.ScreenFIREConfig => "ScreenFIRE 配置",
                IStrings.ScreenFIRERepositoryAtGitHub => $"GitHub 上的 ScreenFIRE 存储库",

                IStrings.GNUGeneralPublicLicensev3_0___ => $"GNU 通用公共许可证 v3.0 {Common.Ellipses}",

                IStrings.Screenshot => "截屏",
                IStrings.SavingOptions => "保存选项",
                IStrings.About => "对这个",

                IStrings.Version => "版本",
                IStrings.Phase => "阶段",
                IStrings.Public => "公共",
                IStrings.Beta => "测试版",
                IStrings.Development => "发展",
                IStrings.MadeWith_NET_GTK_ => "使用 .NET 和 GTK# 制作",

                IStrings.OK => "好的",
                IStrings.Yes => "是的",
                IStrings.No => "不",
                IStrings.Cancel => "取消",

                IStrings.ChooseHowYouWouldLikeToFireYourScreenshot_ => "选择您希望如何触发屏幕截图！",
                IStrings.FiredAScreenshot_ => "发了截图！",
                IStrings.ThisButtonHasBeenClicked => "此按钮已被点击",
                IStrings.times_1 => "次",
                IStrings.times_2 => "次",

                IStrings.SomethingWentWrong___ => $"出了些问题{Common.Ellipses}",

                IStrings.ViewScreenshots => $"查看屏幕截图{Common.Ellipses}",
                IStrings.AllMonitors => "所有显示器",
                IStrings.MonitorAtPointer => "在指针处监控",
                IStrings.WindowAtPointer => "指针处的窗口",
                IStrings.ActiveWindow => "活动窗口",
                IStrings.FreeAreaSelection => "区域选择",

                IStrings.SaveAs___ => $"另存为{Common.Ellipses}",
                IStrings.FileAlreadyExists_ => "文件已存在。",
                IStrings.alreadyExists => "已经存在",
                IStrings.WouldYouLikeToReplaceTheExistingFile_ => "您想替换现有文件吗？",

                IStrings.AutoDelete1MonthOldFiles => "自动删除 1 个月前的文件",
                IStrings.Original => "原来的",
                IStrings.Quality => "质量",
                IStrings.Efficiency => "效率",
                IStrings.Animated => "动画",
                IStrings.Video => "视频",

                IStrings.ClicktoCopy___ => $"点击复制{Common.Ellipses}",

                IStrings.FillColor => "填色",
                IStrings.BorderColor => "边框颜色",


                //! Fallback to English.
                _ => En(Name)
            };

        #endregion


    }
}
