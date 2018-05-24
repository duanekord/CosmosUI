using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace cosmosui.Models
{
    public class FieldMasterInfo
    {
        //Place the attribute [Dropdown] above each property that you want to show up as a dropdown.

        private Dictionary<string, IEnumerable<object>> _pop;
        private Dictionary<string, string> _store;


        public FieldMasterInfo()
        {
            _pop = new Dictionary<string, IEnumerable<object>>();
            _store = new Dictionary<string, string>();
        }


        /// <summary>
        /// Gets or sets the field id (field name).
        /// </summary
        [Dropdown]
        public string FieldId { get; set; }

        /// <summary>
        /// Gets or sets the form id (legacy property).
        /// </summary>
        public int FormId { get; set; }

        /// <summary>
        /// Gets or sets the domain name (legacy property).
        /// </summary>
        public string DomainName { get; set; }

        /// <summary>
        /// Gets or sets the from country code.
        /// </summary>
        public string FromCountryCode { get; set; }

        /// <summary>
        /// Gets or sets the to country code (legacy property).
        /// </summary
        public string ToCountryCode { get; set; }

        /// <summary>
        /// Gets or sets the custom condition.
        /// Examples of custom condition are tax type code or validation result.
        /// </summary>
        public string MeetsCustomCondition { get; set; }

        /// <summary>
        /// Gets or sets the account type code.
        /// </summary>
        public string AccountTypeCode { get; set; }

        /// <summary>
        /// Gets or sets the field requirement.
        /// </summary>
        public bool? IsRequired { get; set; }

        /// <summary>
        /// Gets or sets the is visible.
        /// </summary>
        public bool? IsVisible { get; set; }

        /// <summary>
        /// Gets or sets the is read only.
        /// </summary>
        public bool? IsReadOnly { get; set; }

        /// <summary>
        /// Gets or sets the min length.
        /// </summary>
        public int? MinLength { get; set; }

        /// <summary>
        /// Gets or sets the max length.
        /// </summary>
        public int? MaxLength { get; set; }

        /// <summary>
        /// Gets or sets the reg ex pattern.
        /// </summary>
        public string RegExPattern { get; set; }

        /// <summary>
        /// Gets or sets the accepted file extensions.
        /// </summary>
        public string AcceptedFileExtensions { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether is active.
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// Gets or sets the label key.
        /// </summary>
        public string LabelKey { get; set; }

        /// <summary>
        /// Gets or sets the input type (legacy property).
        /// </summary>
        public string InputType { get; set; }

        /// <summary>
        /// Gets or sets the place holder key.
        /// </summary>
        public string PlaceHolderKey { get; set; }

        /// <summary>
        /// Gets or sets the help text key.
        /// </summary>
        public string HelpTextKey { get; set; }

        /// <summary>
        /// Gets or sets the parent field key.
        /// </summary>
        public string ParentFieldKey { get; set; }

        /// <summary>
        /// Gets or sets the parent form key (legacy property).
        /// </summary>
        public string ParentFormKey { get; set; }

        /// <summary>
        /// Gets or sets the domain items json (legacy property).
        /// </summary>
        // ReSharper disable once InconsistentNaming
        public string DomainItemsJSON { get; set; }

        /// <summary>
        /// Gets or sets the tenant id.
        /// </summary>
        [Dropdown]
        public string TenantId { get; set; }

        /// <summary>
        /// Gets or sets the income type code.
        /// </summary>
        public string IncomeTypeCode { get; set; }

        /// <summary>
        /// Gets or sets who the record was created by.
        /// </summary>
        public string CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the created date.
        /// </summary>
        public string CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets who last updated the record.
        /// </summary>
        public string LastUpdatedBy { get; set; }

        /// <summary>
        /// Gets or sets the last update date.
        /// </summary>
        public string LastUpdateDate { get; set; }

        /// <summary>
        /// Gets or sets the section key.
        /// </summary>
        public string SectionKey { get; set; }

        /// <summary>
        /// Gets or sets the bank country code.
        /// </summary>
        [Dropdown]
        public string BankCountryCode { get; set; }

        /// <summary>
        /// Gets or sets the residence country code.
        /// </summary>
        public string ResidenceCountryCode { get; set; }

        /// <summary>
        /// Gets or sets the mailing address country code.
        /// </summary>
        public string MailingAddressCountryCode { get; set; }

        /// <summary>
        /// Gets or sets the beneficiary address country code.
        /// </summary>
        [Dropdown]
        public string BeneficiaryAddressCountryCode { get; set; }

        /// <summary>
        /// Gets or sets the control type.
        /// </summary>
        public string ControlType { get; set; }

        /// <summary>
        /// Gets or sets custom css classes for an HTML control
        /// </summary>
        public string CssClass { get; set; }

        /// <summary>
        /// Gets or sets the metadata dependency id.
        /// </summary>
        public int? MetadataDependencyId { get; set; }

        /// <summary>
        /// Gets or sets the data dependency id.
        /// </summary>
        public int? DataDependencyId { get; set; }

        /// <summary>
        /// Gets or sets the dynamic section dependency id.
        /// </summary>
        public int? DynamicSectionDependencyId { get; set; }

        /// <summary>
        /// Gets or sets the currency code.
        /// </summary>
        public string CurrencyCode { get; set; }

        /// <summary>
        /// Gets or sets the aria label key.
        /// </summary>
        public string AriaLabelKey { get; set; }

        /// <summary>
        /// Gets or sets the password mask.
        /// </summary>
        public bool? PasswordMask { get; set; }

        /// <summary>
        /// Gets or sets field id for which to compare value
        /// </summary>
        public string MustEqualFieldId { get; set; }

        /// <summary>
        /// Gets or sets the field group.
        /// </summary>
        public string FieldGroup { get; set; }

        /// <summary>
        /// Gets or sets the column hint.
        /// </summary>
        public int? ColumnHint { get; set; }

        /// <summary>
        /// Gets or sets the indent hint.
        /// </summary>
        public int? IndentHint { get; set; }

        /// <summary>
        /// Gets or sets the field info.
        /// </summary>
        public string FieldInfo { get; set; }

        /// <summary>
        /// Gets or sets the null selection text key.
        /// </summary>
        public string NullSelectionTextKey { get; set; }

        /// <summary>
        /// Gets or sets the ordinal number.
        /// </summary>
        public int? OrdinalNumber { get; set; }

        /// <summary>
        /// Gets or sets the effective on date.
        /// </summary>
        public DateTime? EffectiveOn { get; set; }

        /// <summary>
        /// Gets or sets the effective until date.
        /// </summary>
        public DateTime? EffectiveUntil { get; set; }

        /// <summary>
        /// Gets or sets the document id.
        // ReSharper disable once InconsistentNaming
        public string id { get; set; }

        /// <summary>
        /// Gets or sets the document _rid.
        /// </summary>
        // ReSharper disable once InconsistentNaming
        public string _rid { get; set; }

        /// <summary>
        /// Gets or sets the document link.
        /// </summary>
        // ReSharper disable once InconsistentNaming
        public string _self { get; set; }

        /// <summary>
        /// Gets or sets the _etag, a reference to last update.
        /// </summary>
        // ReSharper disable once InconsistentNaming
        public string _etag { get; set; }

        /// <summary>
        /// Gets or sets the _attachments link.
        /// </summary>
        // ReSharper disable once InconsistentNaming
        public string _attachments { get; set; }

        /// <summary>
        /// Gets or sets the _ts.
        /// </summary>
        // ReSharper disable once InconsistentNaming
        public string _ts { get; set; }

        public Dictionary<string, IEnumerable<object>> populate_dd { get { return _pop; } }

        public Dictionary<string, string> store_dd { get { return _store; } }

    }
}