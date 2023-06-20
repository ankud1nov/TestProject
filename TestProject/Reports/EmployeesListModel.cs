using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;
using System.Windows.Input;
using TestProject.Domain.Extensions;
using TestProject.Domain.Models.Reports;
using TestProject.Reports.Enums;

namespace TestProject.Reports;

public class EmployeesListViewModel : ObservableObject
{
    private readonly ICollection<int> Ages = Enumerable.Range(1, 99).ToArray();
    private readonly ICollection<int> Years = Enumerable.Range(1900, 120).ToArray();

    private int? _selectedEmployeeExperience;
    private ExperienceTypeEnum _selectedEmployeeExperiencesType;
    private bool _selectedEmployeeExperienceIsActive;
    private FilterTypeEnum _selectedFilterType;
    private ICollection<int> _filterValues;
    private int? _selectedFilterValue;

    public ICollectionView EmployeesCollection { get; set; }
    public ICollection<string> Companyes { get; set; }
    public string SelectedCompany { get; set; }
    public ICollection<ExperienceTypeEnum> EmployeeExperiencesType { get; set; }

    public ExperienceTypeEnum SelectedEmployeeExperienceType 
    {
        get => _selectedEmployeeExperiencesType;
        set
        {
            if (SetProperty(ref _selectedEmployeeExperiencesType, value, nameof(SelectedEmployeeExperienceType)))
            {
                ApllyFilters();
                SelectedEmployeeExperienceIsActive = _selectedEmployeeExperiencesType != ExperienceTypeEnum.Skip;
            }                
        }
    }

    public int? SelectedEmployeeExperience 
    {
        get => _selectedEmployeeExperience;
        set
        {
            if (SetProperty(ref _selectedEmployeeExperience, value, nameof(SelectedEmployeeExperience)))
                ApllyFilters();
        }
    }

    public bool SelectedEmployeeExperienceIsActive
    {
        get => _selectedEmployeeExperienceIsActive;
        set => SetProperty(ref _selectedEmployeeExperienceIsActive, value, nameof(SelectedEmployeeExperienceIsActive));
    }

    public ICollection<FilterTypeEnum> FilterTypes { get; set; }
    public FilterTypeEnum SelectedFilterType 
    {
        get => _selectedFilterType;
        set
        {
            SetProperty(ref _selectedFilterType, value, nameof(SelectedFilterType));
            FilterValues = _selectedFilterType == FilterTypeEnum.Age ? Ages : Years ;
        }
    }

    public ICollection<int> FilterValues
    {
        get => _filterValues;
        set => SetProperty(ref _filterValues, value, nameof(FilterValues));
    }
    public int? SelectedFilterValue
    {
        get => _selectedFilterValue;
        set
        {
            if (SetProperty(ref _selectedFilterValue, value, nameof(SelectedFilterValue)))
                ApllyFilters();
        }
    }

    public ICommand ClearFilterValueCommand { get; }

    public EmployeesListViewModel(ICollection<EmployeesListItemModel> employeesListItems)
    {
        Companyes = employeesListItems.Select(x => x.Company).Distinct().ToList();
        SelectedCompany = Companyes.FirstOrDefault();
        EmployeeExperiencesType = EnumExtensions.GetValues<ExperienceTypeEnum>().ToArray();
        SelectedEmployeeExperienceType = ExperienceTypeEnum.Skip;
        FilterTypes = EnumExtensions.GetValues<FilterTypeEnum>().ToArray();
        SelectedFilterType = FilterTypeEnum.Age;

        EmployeesCollection = CollectionViewSource.GetDefaultView(employeesListItems);

        ApllyFilters();

        ClearFilterValueCommand = new RelayCommand(ClearFilterValue);
    }

    public void ApllyFilters()
    {
        if (EmployeesCollection == null)
            return;

        Predicate<object> predicate =
            (x =>
            {
                EmployeesListItemModel item = x as EmployeesListItemModel;
                var primaryFieldsFilled = !string.IsNullOrEmpty(SelectedCompany) 
                    && (SelectedEmployeeExperienceType == ExperienceTypeEnum.Skip 
                        || SelectedEmployeeExperienceType != ExperienceTypeEnum.Skip && SelectedEmployeeExperience != null);
                var companyEquals = item.Company == SelectedCompany;
                var experience = SelectedEmployeeExperienceType == ExperienceTypeEnum.MoreThen
                                ? item.EmployeeExperience.Days > SelectedEmployeeExperience
                                : SelectedEmployeeExperienceType == ExperienceTypeEnum.LessThen
                                    ? item.EmployeeExperience.Days < SelectedEmployeeExperience
                                    : SelectedEmployeeExperienceType == ExperienceTypeEnum.Equals
                                        ? item.EmployeeExperience.Days == SelectedEmployeeExperience
                                        : true;
                var notPrimaryFields = SelectedFilterValue == null || (SelectedFilterType == FilterTypeEnum.Age
                                        ? item.EmployeeAge == SelectedFilterValue
                                        : SelectedFilterType != FilterTypeEnum.Birthdate || item.BirthdateYear == SelectedFilterValue);

                return primaryFieldsFilled && companyEquals && experience && notPrimaryFields;
            });
        EmployeesCollection.Filter = predicate;
    }

    public void ClearFilterValue()
    {
        SelectedFilterValue = null;
    }
}
