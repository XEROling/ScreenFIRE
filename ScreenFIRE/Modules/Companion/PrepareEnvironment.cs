﻿using ScreenFIRE.Assets;
using env = System.Environment;

namespace ScreenFIRE.Modules.Companion {

    /// <summary> Prepare the environment for ScreenFIRE </summary>
    internal static class PrepareEnvironment {
        public static bool Run() {

            //? User profile is missing
            if (!Directory.Exists(Common.UserProfile))
                return false;

            //! (User) >> Pictures
            if (!Directory.Exists(env.GetFolderPath(env.SpecialFolder.MyPictures)))
                Directory.CreateDirectory(
                            env.GetFolderPath(env.SpecialFolder.MyPictures));

            //! (User) >> Pictures >> ScreenFIRE
            if (!Directory.Exists(Common.SF))
                Directory.CreateDirectory(Common.SF);

            //! (System App Data) >> ScreenFIRE
            if (!Directory.Exists(Common.SF_Data))
                Directory.CreateDirectory(Common.SF_Data);

            //! SaveOptions
            if (string.IsNullOrEmpty(Common.LocalSave_Settings.Location))
                Common.LocalSave_Settings.Location = Common.SF;

            //! Rebuild previously fetched strings according to current language
            Strings.RebuildStorage(Languages.SystemLanguage());

            //! Delete 1 month old screenshots
            foreach (var DateDir in Directory.EnumerateDirectories(Common.LocalSave_Settings.Location))
                foreach (var file in Directory.EnumerateFiles(DateDir))
                    if (new FileInfo(file).CreationTime < DateTime.Now.AddMonths(-1))
                        File.Delete(file);

            return true;
        }
    }
}
