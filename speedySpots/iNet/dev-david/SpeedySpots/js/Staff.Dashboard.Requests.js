var StaffDashboardRequests = function () {
    var _listWrap = '';
    var _requestsTable = '';
    var _tableBody = '';
    var _numbRecords = '';
    var _prevButton = '';
    var _nextButton = '';
    var _currentPageInput = '';
    var _iCurrentPage = 1;
    var _itemsPerPageInput = '';
    var _totalPages = '';
    var _goToPage = '';
    var _goChangePerPage = '';
    var _itemCountText = '';
    var _itemsPerPageTracker = '';
    var _itemsPerPage = 5;
    var _totalRecords = 0;
    var _sortName = 'IARequestID';
    var _sortDirection = 'DESC';
    var _sortNameTracker = '';
    var _sortDirTracker = '';
    var _multiSelectOptions = { selectAll: false, oneOrMoreSelected: '*', noneSelected: '' };

    function Init(intialPageNumber, initalItemsPerPageTracker, initialSortNameTracker, initialSortDirTracker) {
        _listWrap = $('#listWrap');
        _requestsTable = _listWrap.find('#requests');
        _tableBody = _requestsTable.find('tbody');
        _prevButton = _listWrap.find('#prevButton');
        _nextButton = _listWrap.find('#nextButton');
        _currentPageInput = _listWrap.find('#currentPage');
        _itemsPerPageInput = _listWrap.find('#itemsPerPage');
        _totalPages = _listWrap.find('#totalPages');
        _goToPage = _listWrap.find('#goToPage');
        _goChangePerPage = _listWrap.find('#goChangePerPage');
        _itemCountText = _listWrap.find('#itemCountText');

        _itemsPerPageTracker = initalItemsPerPageTracker;
        _itemsPerPage = parseInt(_itemsPerPageTracker.attr('value'));
        
        _iCurrentPage = intialPageNumber;

        _sortNameTracker = initialSortNameTracker;
        _sortDirTracker = initialSortDirTracker;
        _sortName = _sortNameTracker.attr('value');
        _sortDirection = _sortDirTracker.attr('value');

        WireupEvents();
        UpdatePageNumberingDetails();
        InitializeDefaultSortedColumn();
    };

    function WireupEvents() {
        _prevButton.click(function (e) {
            e.preventDefault();
            var newPage = _iCurrentPage - 1;
            SetPage(newPage);
        });
        
        _nextButton.click(function (e) {
            e.preventDefault();
            var newPage = _iCurrentPage + 1;
            SetPage(newPage);
        });

        _goToPage.click(function (e) {
            e.preventDefault();
            GoToNewPageClick();
        });

        _currentPageInput.keypress(function(e) {
            if(e.which == 13) {
                e.preventDefault();
                GoToNewPageClick();
            }
        });

        _goChangePerPage.click(function (e) {
            e.preventDefault();
            SetNewPageSize();
        });

        _itemsPerPageInput.keypress(function(e) {
            if(e.which == 13) {
                e.preventDefault();
                SetNewPageSize();
            }
        });

        _requestsTable.find('thead tr a').click(function (e) {
            e.preventDefault();
            SortRequests($(this));
        });

    };

    function SetPage(pageNumber) {
        _iCurrentPage = pageNumber;

        LoadRequests();
    };

    function LoadRequests(){
        ShowOverlay();

        $.ajax({
            url: 'ajax-staff-requests-dashboard.aspx',
            type: 'POST',
            data: { 
                    PageSize: _itemsPerPage, 
                    PageNumber: _iCurrentPage, 
                    OrderBy: _sortName + ' ' + _sortDirection },
            dataType: 'html',
            cache: false,
            success: function (data) {
                _tableBody.html(data);                

                UpdatePageNumberingDetails();
            },
        });
    };

    function ShowOverlay() {
        var overlayTemplate = '<tr class="overlay"><td colspan="7"><img src="img/LoadingIcon.gif" class="loading_circle" alt="Loading" /><br />Loading...</td></tr>';
        _tableBody.html(overlayTemplate);
    };

    function UpdatePageNumberingDetails() {
        _numbRecords = _tableBody.find('#numbRecords');
        _totalRecords = parseInt(_numbRecords.html());

        ShowHideButtons();
        SetPageNumberingText();

        _currentPageInput.attr('value', _iCurrentPage);
        _itemsPerPageInput.attr('value', _itemsPerPage);
    }

    function ShowHideButtons() {
        if(_iCurrentPage > 1) {
            _prevButton.show();            
        } else {
            _prevButton.hide();
        }

        if(_iCurrentPage + 1 <= GetTotalPages()){
            _nextButton.show();
        } else {
            _nextButton.hide();
        }

        _totalPages.html(GetTotalPages());
    };

    function GetTotalPages() {
        var totalPages = Math.ceil(_totalRecords / _itemsPerPage);
        if(totalPages<=0) {
            totalPages = 1;
        }

        return totalPages;
    };

    // Sending the user to their desired page
    function GoToNewPageClick() {
        var pageNumber = parseInt(_currentPageInput.attr('value').replace(/\D/g, '' ));

        if(pageNumber > GetTotalPages()) {
            pageNumber = GetTotalPages();
        } else if(pageNumber <= 0) {
            pageNumber = 1;
        }

        SetPage(pageNumber);
    };

    function SetNewPageSize() {
        var itemsPerPage = parseInt(_itemsPerPageInput.attr('value').replace(/\D/g, '' ));

        if(itemsPerPage > 500) {
            itemsPerPage = 500;
        } else if(itemsPerPage <= 0) {
            itemsPerPage = 50;
        }

        _itemsPerPage = itemsPerPage;
        _itemsPerPageTracker.attr('value', _itemsPerPage);

        SetPage(1);
    };

    function SetPageNumberingText() {
        var lastRecord = (_iCurrentPage * _itemsPerPage);
        var firstRecord = (lastRecord - _itemsPerPage) + 1;

        if(firstRecord <= 0) {
            firstRecord = 1;
        }

        if(lastRecord >_totalRecords) {
            lastRecord = _totalRecords;
        }
        
        if(_totalRecords > 0) {
            _itemCountText.html('Requests ' + firstRecord + ' to ' + lastRecord + ' of ' + _totalRecords);
        } else {
            _itemCountText.html('No Requests');
        }
    };

    function InitializeDefaultSortedColumn() {
        var sortedItem = _requestsTable.find('thead tr a[data-sortName="' + _sortName + '"]');
        if(_sortDirection === 'DESC') {
            sortedItem.parent().addClass('rgSortedDESC');
        } else {
            sortedItem.parent().addClass('rgSortedASC');
        }
    }

    function SortRequests(sortSelection) {
        var previousSortName = _sortName;

        _sortName = sortSelection.attr('data-sortName');
        _sortDirection = sortSelection.attr('data-sortDirection');

        if(previousSortName != _sortName) {
            // Reset the last column
            var previousSortItem = _requestsTable.find('thead tr a[data-sortName="' + previousSortName + '"]');
            previousSortItem.attr('data-sortDirection', previousSortItem.attr('data-orginalSortDir'));
            previousSortItem.parent().removeClass('rgSortedDESC');
            previousSortItem.parent().removeClass('rgSortedASC');
        }

        if(_sortDirection === 'ASC') {
            sortSelection.attr('data-sortDirection', 'DESC');
            sortSelection.parent().removeClass('rgSortedDESC');
            sortSelection.parent().addClass('rgSortedASC');
        } else {
            sortSelection.attr('data-sortDirection', 'ASC');
            sortSelection.parent().removeClass('rgSortedASC');
            sortSelection.parent().addClass('rgSortedDESC');
        }

        _sortNameTracker.attr('value', _sortName);
        _sortDirTracker.attr('value', _sortDirection);

        LoadRequests();
    };

    function RemoveLastComma(sValue) {
        return sValue.replace(/,$/,"");
    };

    function ProcessCheckFilter(chkStatus, sID) {
        var m_cboFilter = $find(sID);

        var sText = "";
        var sValues = "";
        var oItems = m_cboFilter.get_items();

        for(var i = 0; i < oItems.get_count(); i++) {
            var oItem = oItems.getItem(i);
        
            //get the checkbox element of the current item
            var m_chkStatus = $get(m_cboFilter.get_id() + "_i" + i + "_m_chkStatus");
            if (m_chkStatus.checked) {
                sText += oItem.get_text() + "," ;
                sValues += oItem.get_value() + ",";
            }
        }

        sText = RemoveLastComma(sText);
        sValues = RemoveLastComma(sValues);
    
        if(sText.length > 0) {
            m_cboFilter.set_text(sText);
            m_cboFilter.set_value(sValues);
        } else {
            m_cboFilter.set_text("");
            m_cboFilter.set_value("");
        }
    };


    /* -- Language Filters --*/
    function MultiSelectFilterConfigure(fieldWrap, hiddenField, selectField) {
        SetFilterPreSelectedItems(fieldWrap, hiddenField);
        WireupMultiselect(selectField, hiddenField);
    };

    function SetFilterPreSelectedItems(fieldWrap, hiddenField) {
        var arrayItems = new Array();
        arrayItems = hiddenField.attr('value').split(',');

        fieldWrap.find('option').each(function (i) {
            if(detectItem(arrayItems, $(this).attr('value'))) {
                $(this).attr('selected', 'selected');
            }
        });
    };

    function WireupMultiselect(selectField, hiddenField) {
        selectField.multiSelect(_multiSelectOptions, function(e) {
            SetSelectedFilterValue(hiddenField, e);
        });
    };
        
    function SetSelectedFilterValue(trackingField, inputField) {
        var itemValue = inputField.attr('value');
        var arrayItems = new Array();
        arrayItems = trackingField.attr('value').split(',');
        
        if(inputField.attr('checked') == true) {
            if(!detectItem(arrayItems, itemValue)) {
                arrayItems.push(itemValue);
                trackingField.attr('value', arrayItems.toString());
            }
        } else {
            if(detectItem(arrayItems, itemValue)) {
                arrayItems = removeItem(arrayItems, itemValue);
                trackingField.attr('value', arrayItems.toString());
            }
        }
    };

    function detectItem(originalArray, itemToDetect) {
	    var j = 0;
	    while (j < originalArray.length) {
		    if (originalArray[j] == itemToDetect) {
			    return true;
		    } else { j++; }		
	    }
	    return false;
    }

    //remove item (string or number) from an array
    function removeItem(originalArray, itemToRemove) {
	    var j = 0;
	    while (j < originalArray.length) {
		    if (originalArray[j] == itemToRemove) {
			    originalArray.splice(j, 1);
	        } else { j++; }
        }
        return originalArray;
    };

    return {
        Init: Init,
        ProcessCheckFilter : ProcessCheckFilter,
        MultiSelectFilterConfigure: MultiSelectFilterConfigure
    }
} ();