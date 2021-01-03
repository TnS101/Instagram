using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Domain.Database;
using Application.ApplicationLayer.Services.Posts;
using System.Collections.Generic;
using Application.ApplicationLayer.Common;

namespace Application.ApplicationLayer.ViewModels
{
    public class PostViewModel : ViewModelBase
    {
        #region RelayCommands
        private RelayCommand<string> createCommand;
        #endregion

        #region Services
        private readonly PostWrites postWrites;
        #endregion

        public PostViewModel(string userId)
        {
            UserId = userId;

            postWrites = new PostWrites(new DbContext());
        }

        public string UserId
        {
            get
            {
                return userId;
            }

            set
            {
                userId = value;
                RaisePropertyChanged(nameof(UserId));
            }
        }

        private string userId;

        public string SelectedImagePath
        {
            get
            {
                return selectedImagePath;
            }

            set
            {
                selectedImagePath = value;
                RaisePropertyChanged(nameof(SelectedImagePath));
            }
        }

        private string selectedImagePath;

        public string Description
        {
            get
            {
                return description;
            }

            set
            {
                description = value;
                RaisePropertyChanged(nameof(Description));
            }
        }

        private string description;

        public List<string> GaleryImagePaths
        {
            get
            {
                galeryImagePaths = LoadPaths();
                return galeryImagePaths;
            }

            set
            {
                galeryImagePaths = value;
                RaisePropertyChanged(nameof(GaleryImagePaths));
            }
        }

        private List<string> galeryImagePaths;

        public RelayCommand<string> CreateCommand
        {
            get
            {
                return createCommand ??= new RelayCommand<string>(textField =>
                 {
                     postWrites.Create(UserId, Description, SelectedImagePath);
                 }, textField => !string.IsNullOrWhiteSpace(textField));
            }
        }

        private List<string> LoadPaths(int amount = 17)
        {
            var result = new List<string>();

            for (int i = 0; i < amount; i++)
            {
               result.Add(FileManager.GetPath($"dog{i}"));
            }

            return result;
        }
    }
}
