var jqGridHelper = {};
var currentCulture = currentCulture || "";
var language = language || "";
var dateFormat = dateFormat || "";
var timeFormat = timeFormat || "";
var dateTimeFormat = dateTimeFormat || "";
var printDateFormat = printDateFormat || "";
var printLongDateFormat = printLongDateFormat || "";
var printTimeFormat = printTimeFormat || "";
var printDateTimeFormat = printDateTimeFormat || "";
var printLongDateTimeFormat = printLongDateTimeFormat || "";
var detailsTextFormat = detailsTextFormat || "View Details Of {0}";

// Init grid options
var pageSize = pageSize || 50;
var gridRowList = gridRowList || "";
var gridRows = $.map(gridRowList.split(','), function (value) {
    return parseInt(value, 10);
});
if ($.inArray(pageSize, gridRows) == -1) {
    gridRows.push(pageSize);
}
gridRows.sort(function (a, b) { return a - b; });

//Getting page size
var pageSize = pageSize || 50;
var gridRowList = gridRowList || "";
var gridRows = $.map(gridRowList.split(','), function (value) {
    return parseInt(value, 10);
});
if ($.inArray(pageSize, gridRows) == -1) {
    gridRows.push(pageSize);
}

/* Show hide long text */
var moreText = moreText || "More...";
var lessText = lessText || "Less...";
$("body").on("click", ".grid-more", function () {
    if ($(this).siblings(".grid-full-text").css('display') != 'none') {
        $(this).text(lessText).siblings(".grid-full-text").hide();
        $(this).text(moreText).siblings(".grid-short-text").show();
    } else {
        $(this).text(moreText).siblings(".grid-full-text").show();
        $(this).text(lessText).siblings(".grid-short-text").hide();
    }
    // Calculate size header column
    var grids = $('table').find('.grid-more');
    for (var i = 0; i < grids.length; i++) {
        jqGridHelper.updateSizeColumnHeaders('#' + $($('.grid-more').closest('table')).attr('id'));
    }
});

(function (context) {

    //Format date time for jqGrid
    context.convertToJqGridFormat = function (input) {
        /*
         * Replace C# Date time format to jqgrid date time format
            * Replace the minute format first from m to i
            * Replace the time format first from t to A
        */

        //Remove duplicate characters in format string
        var format = siteHelper.removeDuplicate(input);

        //Replace m to i
        format = format.replace(/m/g, "i");

        //Replace t to A
        format = format.replace(/t/g, "A");

        //Replace y to yy
        format = format.replace(/y/g, "Y");

        return format;
    };

    //Get jqGrid date time format
    context.getJqgridDateTimeFormat = function () {
        return context.convertToJqGridFormat(dateTimeFormat);
    };

    //Get jqGrid date format
    context.getJqgridDateFormat = function () {
        return context.convertToJqGridFormat(dateFormat);
    };

    // Get jqgrid time format
    context.getJqgridTimeFormat = function () {
        return context.convertToJqGridFormat(timeFormat);
    };

    // Print short date none tooltip
    context.printShortDate = function (value) {
        var time = siteHelper.getLocalAndUTCTime(value);
        if (time != null) {
            var localTimeText = Globalize.format(time.local, printDateTimeFormat);
            return "<span>" + localTimeText + "</span>";
        }
        return "";
    };

    // Print date
    context.printDate = function (value, format, isHtmlFormat) {
        var time = siteHelper.getLocalAndUTCTime(value);
        if (time != null) {
            var localTimeText = Globalize.format(time.local, format);
            var utcTimeText = Globalize.format(time.utc, format);
            if (isHtmlFormat != undefined && isHtmlFormat == false) {
                return localTimeText;
            }

            return "<span data-rel='tooltip' data-title='" + utcTimeText + "'>" + localTimeText + "</span>";
        }
        return "";
    };

    // Print local date time
    context.printLocalDateTimeFormat = function (value, isHtmlFormat) {
        return context.printDate(value, printDateTimeFormat, isHtmlFormat);
    };

    // Print local long date time
    context.printLocalLongDateTimeFormat = function (value) {
        return context.printDate(value, printLongDateTimeFormat);
    };

    // Print local date
    context.printLocalDateFormat = function (value) {
        return context.printDate(value, printDateFormat);
    };

    // Print local long date
    context.printLocalLongDateFormat = function (value) {
        return context.printDate(value, printLongDateFormat);
    };

    // Print local time
    context.printLocalTimeFormat = function (value) {
        return context.printDate(value, printTimeFormat);
    };

    // Print custom date time format
    context.printDateTimeFormat = function (value, format) {
        return context.printDate(value, format);
    };

    //style color
    context.styleColor = function (value) {
        if (value == null || value == "")
            return "";

        if (value == "#FFFFFF" || value == "#ffffff" || value == "#FFF" || value == "fff") {
            // Background is white
            return "<span class='status-color' style='color: #393939; background-color:" + value + ";'>" + value + "</span>";
        }

        return "<span class='status-color' style='background-color:" + value + ";'>" + value + "</span>";
    };

    //style label
    context.styleLabel = function (value, compareValue, trueMessage, falseMessage, trueClass, falseClass) {

        if (trueClass == null || trueClass == "") {
            trueClass = "label-success";
        }

        if (falseClass == null || falseClass == "") {
            falseClass = "";
        }

        var icon = "";
        if (value == compareValue) {
            if (trueMessage != null && trueMessage != '') {
                icon = "<span class='label " + trueClass + "'> " + trueMessage + " </span>";
            }
        } else {
            if (falseMessage != null && falseMessage != '') {
                icon = "<span class='label " + falseClass + "'> " + falseMessage + " </span>";
            }
        }
        return icon;
    };

    //Format link
    context.formatLink = function (url, text, tooltip, isDetailsLink, openNewTab) {
        var template = "<a class='grid-action' target='{0}' href='{1}'><div class='ui-pg-div' title='{2}'> {3} </div></a>";

        if (url == null || url == '') {
            return "";
        }

        //Set target
        if (openNewTab == null) {
            openNewTab = false;
        }
        var target = "_self";
        if (openNewTab) {
            target = "_blank";
        }

        //Set text
        if (text == null || text.toString() == '') {
            text = url;
        }

        //Format tooltip
        if (tooltip == null) {
            tooltip = '';
        }
        if (isDetailsLink && tooltip != '') {
            tooltip = detailsTextFormat.format(tooltip);
        }

        return template.format(target, url, tooltip, text);
    };

    // Build show / hide long content
    context.formatLongText = function (text, length, moreStr) {
        if (length == undefined || length <= 0) {
            length = 60;
        }
        if (text != null && text.length > length) {
            var regex = /(<([^>]+)>)/ig;
            var textRegex = text.replace(regex, " ").replace(/(\w+;)/g, " ");
            if (textRegex != null && textRegex.length > length) {
                var fullHtml = "<div><span class='grid-short-text'>{0}</span> <span class='grid-full-text' style='display: none'>{1}</span> <span class='grid-more'>{2}</span></div>";
                var shortText = textRegex.substring(0, length - 1);
                moreText = moreStr || moreText;

                // Check if text is email
                if (siteHelper.isEmail(text)) {
                    shortText = "<a href='mailto:" + text + "' data-rel='tooltip' data-title='" + text + "'>" + shortText + "</a>";
                    text = "<a href='mailto:" + text + "' data-rel='tooltip' data-title='" + text + "'>" + text + "</a>";
                }

                fullHtml = fullHtml.format(shortText, text, moreText).replace(/"/g, "'");
                return fullHtml;
            }
            else {
                return textRegex || "";
            }
        }
        else {
            // Check if text is email
            if (siteHelper.isEmail(text)) {
                text = "<a href='mailto:" + text + "' data-rel='tooltip' data-title='" + text + "'>" + text + "</a>";
            }
            return text || "";
        }
    };

    //Build json value
    context.buildJsonValue = function (jsonString, text) {
        if (text == null) {
            text = "value";
        }
        var canConvertToJson = false;
        try {
            var object = JSON.parse(jsonString);
            canConvertToJson = true;

            // Handle non-exception-throwing cases:
            // Neither JSON.parse(false) or JSON.parse(1234) throw errors, hence the type-checking,
            // but... JSON.parse(null) returns 'null', and typeof null === "object", 
            // so we must check for that, too.

            if (object && $.isArray(object) && object.length == 1) {
                object = object[0];
            }

            var tableId = siteHelper.guid();
            var anchorId = siteHelper.guid();

            if (object && typeof object === "object" && object !== null) {
                var result = "<div class='center'><a id='" + anchorId + "' href='javascript:expandJsonValue(\"#" + anchorId + "\", \"#" + tableId + "\", \"" + text + "\")'>Click to show " + text + "</a></div><table id='" + tableId + "' class='json-table' style='display:none'>";
                for (var key in object) {
                    if (object.hasOwnProperty(key)) {
                        var value = object[key];
                        if (value != null) {
                            if (typeof value !== "object") {
                                if (value.length > 50) {
                                    value = value.substring(0, 50) + "...";
                                }
                                value = encodeURI(value);
                            }
                            else {
                                value = "Complex Settings";
                            }
                        } else {
                            value = "";
                        }

                        result += "<tr><td>" + siteHelper.toCamelFriendly(key) + "</td><td></td><td><b>" + value + "</b></td></tr>";
                    }
                }
                result += "</table>";
                return result;
            }
        } catch (e) {
            if (canConvertToJson) {
            }
        }

        return "<b>" + jsonString + "</b>";
    };

    //Reload grid
    context.reloadGrid = function (selector) {
        $(selector).trigger('reloadGrid');
    };

    //Remove tooltip grid
    context.removeTooltip = function () {
        $(".tooltip.fade.top.in").remove();
    };

    //Remove tooltip grid
    context.showTooltip = function (title, text) {
        return "<span data-rel='tooltip' data-title='" + title + "'>" + text + "</span>";
    };

    // Show loading
    context.showLoading = function () {
        $(".loading").css("display", "block");
    };

    // Show loading
    context.hideLoading = function () {
        $(".loading").css("display", "none");
    };

    //it causes some flicker when reloading or navigating grid
    //it may be possible to have some custom formatter to do this as the grid is being created to prevent this
    //or go back to default browser checkbox styles for the grid
    context.styleCheckbox = function (table) {
        $(table).find('input:checkbox').addClass('ace')
            .wrap('<label />')
            .after('<span class="lbl align-top" />');


        $('.ui-jqgrid-labels th[id*="_cb"]:first-child')
        .find('input.cbox[type=checkbox]').addClass('ace')
        .wrap('<label />').after('<span class="lbl align-top" />');
    };

    //unlike navButtons icons, action icons in rows seem to be hard-coded
    //you can change them like this in here if you want
    context.updateActionIcons = function (table) {
        /*
        var replacement = 
        {
            'ui-icon-pencil' : 'fa fa-pencil blue',
            'ui-icon-trash' : 'fa fa-trash red',
            'ui-icon-disk' : 'fa fa-ok green',
            'ui-icon-cancel' : 'fa fa-remove red'
        };
        $(table).find('.ui-pg-div span.ui-icon').each(function(){
            var icon = $(this);
            var $class = $.trim(icon.attr('class').replace('ui-icon', ''));
            if($class in replacement) icon.attr('class', 'ui-icon '+replacement[$class]);
        });
        */
    };

    //replace icons with FontAwesome icons like above
    context.updatePagerIcons = function (table) {
        var replacement =
        {
            'ui-icon-seek-first': 'fa fa-angle-double-left bigger-140',
            'ui-icon-seek-prev': 'fa fa-angle-left bigger-140',
            'ui-icon-seek-next': 'fa fa-angle-right bigger-140',
            'ui-icon-seek-end': 'fa fa-angle-double-right bigger-140'
        };
        $('.ui-pg-table:not(.navtable) > tbody > tr > .ui-pg-button > .ui-icon').each(function () {
            var icon = $(this);
            var $class = $.trim(icon.attr('class').replace('ui-icon', ''));

            if ($class in replacement) icon.attr('class', 'ui-icon ' + replacement[$class]);
        });
    };


    /*
     * Update header title of columns in Jqgrid
     */
    context.updateSizeColumnHeaders = function (gridName, isResize) {
        setTimeout(function () {
            var divGrid = gridName.replace('#', '#gbox_');
            var row = $(gridName).find('tr')[1];
            var cols = $(row).find('td');
            for (var i = 0; i < cols.length; i++) {
                var width = 0;
                if ($(cols[i]) != null && $(cols[i]).css('display') != 'none') {
                    // Get column which is belong col model
                    if ($(cols[i]).attr('aria-describedby') != undefined) {
                        // Get columns header
                        var col = $(divGrid).find('.ui-jqgrid-labels').find('th')[i];
                        // Get name of element of column with atrribute is aria-describedby
                        var idElement = '#' + $(cols[i]).attr('aria-describedby');
                        width = $(cols[i]).width() + 9; // 9 is padding width
                        // Set width for subgird colum, it not auto scale because it's just small icon
                        if ($(col).attr('id') == (gridName.replace('#', '') + '_subgrid')) {
                            $($(gridName).find('.jqgfirstrow').find('td')[i]).removeClass('overright-style-col-firstrow');
                            width = 24;
                            $($(gridName).find('.jqgfirstrow').find('td')[i]).css('width', width);
                        }
                        // Set width for Action column, for it's beauty on UI
                        if ($(col).attr('id') == (gridName.replace('#', '') + '_Actions')) {
                            $($(gridName).find('.jqgfirstrow').find('td')[i]).removeClass('overright-style-col-firstrow');
                            var icons = $($($(row).find('td')[i])).find('a').length;
                            var widthIcons = 10;
                            for (var index1 = 0; index1 < icons; index1++) {
                                widthIcons += $($($($(row).find('td')[i])).find('a')[index1]).width();
                            }

                            var rows = $(gridName).find('tr');
                            for (var idx = 2; idx < rows.length; idx++) {
                                var r = $(gridName).find('tr')[idx];
                                if ($($($(r).find('td')[i])).find('a').length > icons) {
                                    widthIcons = 10;
                                    icons = $($($(r).find('td')[i])).find('a').length;

                                    for (var index1 = 0; index1 < icons; index1++) {
                                        widthIcons += $($($($(r).find('td')[i])).find('a')[index1]).width();
                                    }
                                }
                            }


                            width = widthIcons + (icons * 3);
                            if ($($(rows).find('td')[i]).width() < width && isResize != true) {
                                width = $($(rows).find('td')[i]).width() + 9;
                            }
                            $($(gridName).find('.jqgfirstrow').find('td')[i]).css('width', width);
                        }
                        // Set width for other columns
                        $(idElement).css('width', width);
                        idElement = idElement.replace(gridName.replace('#', '') + '_', '');
                        // Update width of col model
                        jqGridHelper.updateSizeFirstRows(gridName, idElement.replace('#', ''), width);
                    }
                }
            }
        });
    };

    context.updateSizeFirstRows = function (gridName, className, width) {
        var colModels = $(gridName).jqGrid('getGridParam', 'colModel');
        for (var i = 0; i < colModels.length; i++) {
            // Find col model depends classname
            if (colModels[i].name == className) {
                // Update width for col model
                colModels[i].width = width;
                colModels[i].widthOrg = width;
                $($(gridName).find('.jqgfirstrow').find('.' + className)).css('width', width);
                break;
            }
        }
    };

    context.setMaxlengthForColumnHeader = function (gridName) {
        var len = 50;
        if ($(gridName).jqGrid() != null) {
            var divParent = '#gbox_' + gridName.replace('#', '');
            var colsHeaders = $(divParent).find('.ui-jqgrid-labels').find('th');
            for (var i = 0; i < colsHeaders.length; i++) {
                var text = $(colsHeaders[i]).find('div').text();
                if (text != '') {
                    var words = text.split(' ');
                    if (words != null && words.length > 0) {
                        for (var k = 0; k < words.length; k++) {
                            len = jqGridHelper.convertStrToPixel(words[k]);
                            len = len + 11; // for padding
                            $(gridName).jqGrid()[0].grid.headers[i].minWidth = len;
                        }
                    } else {
                        len = jqGridHelper.convertStrToPixel(text);
                        len = len + 11; // for padding
                        $(gridName).jqGrid()[0].grid.headers[i].minWidth = len;
                    }
                } else {
                    $(gridName).jqGrid()[0].grid.headers[i].minWidth = len;
                }
            }
        }
    };

    context.setTextTitleForColumnHeader = function (gridName) {
        if ($(gridName).jqGrid() != null) {
            var divParent = '#gbox_' + gridName.replace('#', '');
            var colsHeaders = $(divParent).find('.ui-jqgrid-labels').find('th');
            for (var i = 0; i < colsHeaders.length; i++) {
                var text = $(colsHeaders[i]).find('div').text();
                if (text != '') {
                    $(colsHeaders[i]).find('div').attr('title', text);
                }
            }
        }
    };

    // Calculate max-length of each word in text for column
    context.findMaxlengthForColumn = function (i, gridName, colModels, maxLength) {
        var returnLength = maxLength;
        if (colModels[i].name != 'subgrid' && colModels[i].name != 'Actions' && colModels[i].name != 'Id') {
            $(gridName).find('tr').each(function (idx, e) {
                if (idx > 0) {
                    var text = $($(e).find('td')[i]).text();
                    if (text != null && text != '') {
                        var words = text.split(' ');
                        if (words != null && words.length > 0) {
                            for (var k = 0; k < words.length; k++) {
                                var len = jqGridHelper.convertStrToPixel(words[k]);
                                len = len + 10;
                                maxLength = maxLength < len ? len : maxLength;
                            }
                        } else {
                            var len = jqGridHelper.convertStrToPixel(text);
                            len = len + 10;
                            maxLength = maxLength < len ? len : maxLength;
                        }
                    }
                    returnLength = maxLength;
                }
            });
        }
        return returnLength;
    };

    /*
     * Update columns content in Jqgrid
     */
    context.updateSizeColumns = function (gridSelector, size, className, idx) {
        //var colModels = $(gridSelector).jqGrid('getGridParam', 'colModel');
        //colModels[idx].width = size;
        //colModels[idx].widthOrg = size;
        // Set size for column i
        $($(gridSelector).find('.jqgfirstrow').find('td')[idx]).css('width', size);

    };

    context.enableTooltips = function (table) {
        $('.navtable .ui-pg-button').tooltip({ container: 'body' });
        $(table).find('.ui-pg-div').tooltip({ container: 'body' });
    };

    //Grid functions
    context.addFilterItem = function (myfilter, field, op, data) {
        if (data != null && data != '') {
            myfilter.rules.push({ field: field, op: op, data: data });
        }
    };

    context.addPostDataFilters = function (groupOp) {
        return { groupOp: groupOp, rules: [] };
    };

    context.changeExportUrl = function ($exportSelector, exportUrl, data) {
        var recursiveEncoded = $.param(data);
        exportUrl = exportUrl + "&" + recursiveEncoded;
        $exportSelector.attr("href", exportUrl);

        var idExportCustom = $exportSelector.attr("id");
        $($('.custom-btn-export').find('#' + idExportCustom)).attr("href", exportUrl);
    };

    context.addFilterToExportUrl = function (exportSelector, exportUrl, data) {
        var newExportUrl = $(exportSelector).attr("href");
        if ($.trim(exportUrl) != $.trim(newExportUrl)) {
            newExportUrl = newExportUrl + "&";
        } else {
            newExportUrl = newExportUrl + "?";
        }
        var rules = $.parseJSON(data).rules;
        if (rules !== undefined && rules != null && rules.length > 0) {
            $.each(rules, function (index, value) {
                if (index == 0) {
                    newExportUrl = newExportUrl + value.field + "=" + value.data;
                } else {
                    newExportUrl = newExportUrl + "&" + value.field + "=" + value.data;
                }
            });
        }
        $(exportSelector).attr("href", newExportUrl);
    };

    context.manualSearch = function (gridSelector, filters, postData) {
        // generate to top postdata filter code
        var grid = $(gridSelector);

        grid.jqGrid({
            // all prarameters which you need
            search: true, // if you want to force the searching
        });

        var currentPostData = grid.jqGrid('getGridParam', 'postData');
        /*
         * Run through current post data
         *      If current post data and new data has same key, use the new one
         *      If new data property is null then remove it in the post data
         *      Other wise prefer the not null new data
         */
        //$.extend(currentPostData, postData);
        $.each(postData, function (i, n) {
            if (currentPostData.hasOwnProperty(i)) {
                if (n != null) {
                    currentPostData[i] = n;
                } else {
                    delete currentPostData[i];
                }
            } else {
                if (n != null) {
                    currentPostData[i] = n;
                }
            }
        });


        grid.jqGrid('setGridParam', { search: filters.rules.length > 0, postData: currentPostData });

        //TODO: set global here is not good, should set for each request
        $.ajaxSettings.traditional = true;
        grid.trigger("reloadGrid", [{ page: 1 }]);
    };

    context.formatSelectCell = function (value) {
        return value ? value.replace(/^-+/, '') : "";
    };

    //switch element when editing inline
    context.select2Switch = function (cellvalue, options, cell) {
        setTimeout(function () {
            $(cell).find('select').select2().addClass("jqgrid-select-inline");
        }, 0);
    };

    //switch element when editing inline
    context.select2SwitchIcon = function (cellvalue, options, cell) {
        setTimeout(function () {
            $(cell).find('select').each(function (i, obj) {
                if (!$(obj).data('select2')) {
                    $(obj).select2({
                        formatResult: function (item) {
                            var icon = "<i class='" + item.text + "'></i> &nbsp;&nbsp;&nbsp;" + item.text;
                            return icon;
                        },
                        formatSelection: function (item) {
                            var icon = "<i class='" + item.text + "'></i> &nbsp;&nbsp;&nbsp;" + item.text;
                            return icon;
                        }
                    }).on("select2-loaded", function (e) {
                    }).addClass("jqgrid-select-inline");
                }
            });
        }, 0);
    };

    //switch element when editing inline
    context.aceSwitch = function (cellvalue, options, cell) {
        setTimeout(function () {
            $(cell).find('input[type=checkbox]').each(function (i, obj) {
                if (!$(obj).hasClass("ace ace-switch ace-switch-5")) {
                    $(obj).wrap('<label class="inline" />')
                        .addClass('ace ace-switch ace-switch-5')
                        .after('<span class="lbl"></span>');
                }
            });
        }, 0);
    };

    //enable datepicker
    context.pickDate = function (cellvalue, options, cell) {
        setTimeout(function () {
            $(cell).find('input[type=text]')
                .datepicker({ format: dateFormat, autoclose: true });
        }, 0);
    };

    context.styleEditForm = function (form) {
        //enable datepicker on "sdate" field and switches for "stock" field
        form.find('input[name=sdate]').datepicker({ format: 'yyyy-mm-dd', autoclose: true })
            .end().find('input[name=stock]')
            .addClass('ace ace-switch ace-switch-5').wrap('<label class="inline" />').after('<span class="lbl"></span>');

        //update buttons classes
        var buttons = form.next().find('.EditButton .fm-button');
        buttons.addClass('btn btn-sm').find('[class*="-icon"]').remove(); //ui-icon, s-icon
        buttons.eq(0).addClass('btn-primary').prepend('<i class="fa fa-check jqgrid-button"></i>');
        buttons.eq(1).prepend('<i class="fa fa-times jqgrid-button"></i>');

        buttons = form.next().find('.navButton a');
        buttons.find('.ui-icon').remove();
        buttons.eq(0).append('<i class="fa fa-chevron-left"></i>');
        buttons.eq(1).append('<i class="fa fa-chevron-right"></i>');

        form.find('select').select2().addClass("jqgrid-select");
    };

    context.styleDeleteForm = function (form) {
        var buttons = form.next().find('.EditButton .fm-button');
        buttons.addClass('btn btn-sm').find('[class*="-icon"]').remove(); //ui-icon, s-icon
        buttons.eq(0).addClass('btn-danger').prepend('<i class="fa fa-trash"></i>');
        buttons.eq(1).prepend('<i class="fa fa-times s"></i>');
    };

    context.styleSearchFilters = function (form) {
        form.find('.delete-rule').val('X');
        form.find('.add-rule').addClass('btn btn-xs btn-primary');
        form.find('.add-group').addClass('btn btn-xs btn-success');
        form.find('.delete-group').addClass('btn btn-xs btn-danger');
    };

    context.styleSearchForm = function (form) {
        var dialog = form.closest('.ui-jqdialog');
        var buttons = dialog.find('.EditTable');
        buttons.find('.EditButton a[id*="_reset"]').addClass('btn btn-sm btn-info').find('.ui-icon').attr('class', 'fa fa-retweet');
        buttons.find('.EditButton a[id*="_query"]').addClass('btn btn-sm btn-inverse').find('.ui-icon').attr('class', 'fa fa-comment-alt');
        buttons.find('.EditButton a[id*="_search"]').addClass('btn btn-sm btn-purple').find('.ui-icon').attr('class', 'fa fa-search');
    };

    context.beforeDeleteCallback = function (e) {
        var form = $(e[0]);
        if (form.data('styled')) return false;

        form.closest('.ui-jqdialog').find('.ui-jqdialog-titlebar').wrapInner('<div class="widget-header" />');
        jqGridHelper.styleDeleteForm(form);

        form.data('styled', true);
    };

    context.beforeEditCallback = function (e) {
        var form = $(e[0]);
        form.closest('.ui-jqdialog').find('.ui-jqdialog-titlebar').wrapInner('<div class="widget-header" />');
        jqGridHelper.styleEditForm(form);
    };

    //Setup grid in tab
    context.setupTabGrid = function (gridSelector, pagerSelector, options, gridSettings, tabSelector, baseExportUrl, moduleName) {
        //Setup add button
        $(".tab-add-button").hover(function () {
            $(this).html("+");
        }, function () {
            var total = $(this).data("total");
            $(this).text(total);
        });

        //Setup toggle button
        $('a[data-toggle="tab"]').on('shown.bs.tab', function (e) {
            if ($(e.target).attr('href') == tabSelector) {
                $(window).trigger("resize.jqGrid");
                var getIdGrid = ($(e.currentTarget.firstElementChild).attr('id')).replace('tab-', '#');
                getIdGrid += '-grid-table';
                var divGrid = getIdGrid.replace('#', '#gbox_');
                // Update width of tables
                $($(divGrid).find('.ui-jqgrid-htable')).css('width', $(divGrid).width());
                $(getIdGrid).css('width', $(divGrid).width());
                // Update header size
                jqGridHelper.updateSizeColumnHeaders(getIdGrid);
            }
        });

        context.setupGrid(gridSelector, pagerSelector, options, gridSettings, undefined, baseExportUrl, moduleName, true);
    };

    //Setup grid
    context.setupGrid = function (gridSelector, pagerSelector, options, gridSettings, navButtonsSetup, baseExportUrl, moduleName, isTabGrid) {
        // Get id of div jqgrid
        var divGrid = gridSelector.replace('#', '#gbox_');

        var emptyMessage = $("<div class='grid-no-data' style='display: none'><span>No data.</span></div>");
        if (gridSettings == undefined || gridSettings == null) {
            gridSettings = {
                Name: "",
                Order: [],
                Columns: []
            };
        }
        //Set default empty for module name
        if (moduleName == undefined || moduleName == null) {
            moduleName = '';
        }

        var completeCallback = options.loadComplete;
        var defaultOptions = {
            datatype: "json",
            height: 'auto',
            rowNum: pageSize,
            pager: pagerSelector,
            rowList: gridRows,
            //toppager: true,
            //cloneToTop: true,
            shrinkToFit: true,
            altRows: true,
            multiboxonly: true,
            autowidth: true,
            viewrecords: true,
            sortable: {
                update: function () {
                    saveGridConfig(gridSelector, gridSettings.Name);
                }
            },
            subGridOptions: {
                plusicon: "fa fa-chevron-right center bigger-110 orange",
                minusicon: "fa fa-chevron-down center bigger-110 orange"
                //openicon: "fa fa-info-circle center bigger-110 blue"
            },
            beforeRequest: function () {
                // Find max length header
                jqGridHelper.setTextTitleForColumnHeader(gridSelector);

                $(gridSelector).addClass('overright-style-grid-table');
                $($(gridSelector).find('.jqgfirstrow')).find('td').each(function (i, e) {
                    $(e).addClass('overright-style-col-firstrow');
                });

                // Update header columns when show details
                $(gridSelector).bind("jqGridSubGridRowExpanded", function () {
                    setTimeout(function () {
                        jqGridHelper.updateSizeColumnHeaders(gridSelector);
                    }, 500);
                });
            },
            loadComplete: function (response) {
                //Setup export url if any
                if (baseExportUrl != undefined && baseExportUrl != null) {
                    var $exportCurrentSearch = $("#exportCurrentSearch" + moduleName);
                    var $exportCurrentPage = $("#exportCurrentPage" + moduleName);
                    var $exportAll = $("#btnExport" + moduleName);

                    var postData = $(gridSelector).jqGrid('getGridParam', 'postData');
                    var exportUrl;

                    //Export search results by current filter
                    if ($exportCurrentSearch.length > 0) {
                        exportUrl = siteHelper.addUrlParam(baseExportUrl, "gridExportMode", "CurrentSearch");
                        jqGridHelper.changeExportUrl($exportCurrentSearch, exportUrl, postData);
                    }

                    //Export search results by current filter and paging
                    if ($exportCurrentPage.length > 0) {
                        exportUrl = siteHelper.addUrlParam(baseExportUrl, "gridExportMode", "CurrentPage");
                        jqGridHelper.changeExportUrl($exportCurrentPage, exportUrl, postData);
                    }

                    //Export all
                    if ($exportAll.length > 0) {
                        exportUrl = siteHelper.addUrlParam(baseExportUrl, "gridExportMode", "All");
                        jqGridHelper.changeExportUrl($exportAll, exportUrl, postData);
                    }
                }

                $('[data-rel=tooltip]').tooltip({ container: 'body' });

                //Remove all available tooltip in grid
                context.removeTooltip();

                var table = this;
                if (response.rows == null || response.rows.length === 0) {
                    $(table).hide();
                    emptyMessage.show();
                } else {
                    $(table).show();
                    emptyMessage.hide();
                }

                setTimeout(function () {
                    jqGridHelper.styleCheckbox(table);
                    jqGridHelper.updateActionIcons(table);
                    jqGridHelper.updatePagerIcons(table);
                    jqGridHelper.enableTooltips(table);
                    jqGridHelper.updateSizeColumnHeaders(gridSelector);
                    jqGridHelper.hiddenFooter(gridSelector);
                    jqGridHelper.buildGridHeader(gridSelector, pagerSelector);
                }, 0);

                // Sync jqGrid filterToolbar and searchGrid filters
                refreshSearchingToolbar($(table), 'cn');

                if (response.ResponseStatus != undefined && response.ResponseStatus != null && response.ResponseStatus == 4) {
                    siteHelper.showUnauthorizeMessage(response.Message);
                }

                if (completeCallback != undefined && completeCallback.length > 0) {
                    completeCallback(response);
                }
            },
            onSortCol: function () {
            },
            resizeStart: function () {

            },
            resizeStop: function (size, index) {
                $($(divGrid).find('.jqgfirstrow')).find('td').each(function (i, e) {
                    if ($(e).css('display') != 'none') {
                        // Remove class fit text size
                        $(e).removeClass('overright-style-col-firstrow');
                    }
                });
                // Remove class for table
                $(gridSelector).removeClass('overright-style-grid-table');

                setTimeout(function () {
                    // Update size of table
                    $(gridSelector).css('width', $($(divGrid).find('.ui-jqgrid-htable')).width());

                    // Update size of columns
                    if ($(gridSelector).jqGrid() != null) {
                        for (var i = 0; i < $(gridSelector).jqGrid()[0].grid.headers.length; i++) {
                            // Just check column model display
                            if (($($(gridSelector).jqGrid()[0].grid.headers[i].el).context.style.display) != 'none') {
                                // Get index column of colModel on UI
                                var idx = $($($(gridSelector).find('tr')).find('td[aria-describedby=' + $(gridSelector).jqGrid()[0].grid.headers[i].el.id + ']')[0]).index();
                                // Get width of column header set to width of column body
                                $($($(gridSelector).find('.jqgfirstrow')).find('td')[idx]).css('width', $(gridSelector).jqGrid()[0].grid.headers[i].width);
                            }
                        }
                    }
                });
            }
        };

        //Get merging grid options
        if (options.sortable !== undefined && !options.sortable) {
            defaultOptions.sortable = false;
        }
        $.extend(true, options, defaultOptions);
        //Update current model via user grid settings
        if (options.colModel != null && options.colModel.length > 0 && gridSettings.Columns != null && gridSettings.Columns.length > 0) {
            // In case grid that used to other pages and difference about number column, we need to check gridSettings.Columns & options.colModel
            // This error just happen when gridSettings.Columns > options.colModel and Jqgrid will be error when options.colModel has column null( the column null belong gridSettings but not existing in colModel
            // We will add columns that are not existing in colModel
            if (gridSettings.Columns.length > options.colModel.length) {
                // Array to store items that are not existing in options.colModel
                var arrItems = [];
                // Find item not existing in options.colModel
                for (var i = 0; i < gridSettings.Columns.length; i++) {
                    var name = gridSettings.Columns[i].Name;
                    var arr = $.grep(options.colModel, function (n, i) {
                        // If found item not existing in options.colModel, return that item
                        return n.name == name ? true : false;
                    });
                    // Push item into arrItems
                    if (arr == null || arr.length == 0) {
                        arrItems.push(i);
                    }
                }
                // For arrItems and add new item into options.colModel & options.colNames
                for (var i = 0; i < arrItems.length; i++) {
                    options.colModel.push({ name: '', index: '', hidden: true });
                    options.colNames.push(name);
                }
            }
            $.each(options.colModel, function (index, value) {
                $.each(gridSettings.Columns, function (i, v) {
                    if (value.name == v.Name) {
                        value.hidden = !v.Show;
                        return false;
                    }
                });
            });
        }

        //Setup grid
        $(gridSelector).jqGrid(options);
        if (gridSettings != null && gridSettings.Order != null) {
            $(gridSelector).jqGrid("remapColumns", gridSettings.Order);
        }

        //Add empty message
        emptyMessage.insertAfter($(gridSelector).parent());

        //Setup frozen columns
        //$(gridSelector).jqGrid('setFrozenColumns');

        /*
         ***************** Setup buttons 
         */
        //enable search/filter toolbar
        if (typeof navButtonsSetup === 'undefined') {
            navButtonsSetup = {
                enableEdit: false,
                enableCreate: false,
                enableDelete: false,
                enableSearch: false,
                enableRefresh: true,
                enableView: true
            };
        }

        if (navButtonsSetup.enableSearch) {
            $(gridSelector).jqGrid('filterToolbar', { defaultSearch: "cn", stringResult: true });
        }

        var navOptions = {
            //cloneToTop: true,
            edit: typeof navButtonsSetup.enableEdit === 'undefined' ? false : navButtonsSetup.enableEdit,
            editicon: 'fa fa-pencil blue',
            add: typeof navButtonsSetup.enableCreate === 'undefined' ? false : navButtonsSetup.enableCreate,
            addicon: 'fa fa-plus-square purple',
            del: typeof navButtonsSetup.enableDelete === 'undefined' ? false : navButtonsSetup.enableDelete,
            delicon: 'fa fa-trash-o red',
            search: typeof navButtonsSetup.enableSearch === 'undefined' ? false : navButtonsSetup.enableSearch,
            searchicon: 'fa fa-search orange',
            refresh: typeof navButtonsSetup.enableRefresh === 'undefined' ? true : navButtonsSetup.enableRefresh,
            refreshicon: 'fa fa-refresh green',
            view: typeof navButtonsSetup.enableView === 'undefined' ? false : navButtonsSetup.enableView,
            viewicon: 'fa fa-search-plus grey'
        };

        var editOptions =
        {
            //edit record form
            closeAfterEdit: true,
            recreateForm: true,
            viewPagerButtons: false,
            beforeShowForm: function (e) {
                var form = $(e[0]);
                form.closest('.ui-jqdialog').find('.ui-jqdialog-titlebar').wrapInner('<div class="widget-header" />');
                jqGridHelper.styleEditForm(form);
            },
            onclickSubmit: function (params, postData) {
                //postData = $.extend({}, postData, editFormPlusData);
                return postData;
            },
            afterSubmit: function (response) {
                var res = $.parseJSON(response.responseText);
                if (res.Success) {
                    return [true, res.Message];
                } else {
                    return [false, res.Message];
                }
            }
        };
        var createOptions = {
            //new record form
            closeAfterAdd: true,
            recreateForm: true,
            viewPagerButtons: false,
            beforeInitData: function () {
                //$(gridSelector).restoreRow(lastSel);
                $(gridSelector).resetSelection();
                if ($.isFunction(window["initCreateData"])) {
                    window["initCreateData"]();
                }
            },
            beforeShowForm: function (e) {
                var form = $(e[0]);
                form.closest('.ui-jqdialog').find('.ui-jqdialog-titlebar').wrapInner('<div class="widget-header" />');
                jqGridHelper.styleEditForm(form);
            },
            onclickSubmit: function (params, postData) {
                //postData = $.extend({}, postData, editFormPlusData);
                postData = $.extend({}, postData, { id: 0 });
                return postData;
            },
            afterSubmit: function (response) {
                var res = $.parseJSON(response.responseText);
                if (res.Success) {
                    return [true, res.Message];
                } else {
                    return [false, res.Message];
                }
            }
        };
        var deleteOptions = {
            //delete record form
            recreateForm: true,
            beforeShowForm: function (e) {
                var form = $(e[0]);
                if (form.data('styled')) return false;

                form.closest('.ui-jqdialog').find('.ui-jqdialog-titlebar').wrapInner('<div class="widget-header" />');
                jqGridHelper.styleDeleteForm(form);

                form.data('styled', true);
            },
            onclickSubmit: function (params, postData) {
                //postData = $.extend({}, postData, editFormPlusData);
                return postData;
            },
            afterSubmit: function (response) {
                var res = $.parseJSON(response.responseText);
                if (res.Success) {
                    return [true, res.Message];
                } else {
                    return [false, res.Message];
                }
            }
        };
        var searchOptions = {
            //search form
            recreateForm: true,
            afterShowSearch: function (e) {
                var form = $(e[0]);
                form.closest('.ui-jqdialog').find('.ui-jqdialog-title').wrap('<div class="widget-header" />');
                jqGridHelper.styleSearchForm(form);
            },
            afterRedraw: function () {
                jqGridHelper.styleSearchFilters($(this));
            },
            recreateFilter: true,
            multipleSearch: true,
            closeOnEscape: true,
            closeAfterSearch: false,
            refreshSearchingToolbar: true,
            sopt: ['eq', 'ne', 'cn', 'bw', 'bn'],
            caption: "Search...",
            Find: "Find",
            Reset: "Reset",
            groupOps: [{ op: "AND", text: "all" }, { op: "OR", text: "any" }],
            matchText: " match",
            rulesText: " rules"
            /**
            multipleGroup:true,
            showQuery: true
            */
        };
        var viewOptions = {
            //view record form
            recreateForm: true,
            beforeShowForm: function (e) {
                var form = $(e[0]);
                form.closest('.ui-jqdialog').find('.ui-jqdialog-title').wrap('<div class="widget-header" />');
            }
        };

        //Setup grid button
        if (pagerSelector != "") {
            //navButtons
            $(gridSelector)
                .jqGrid('navGrid', pagerSelector, navOptions, editOptions, createOptions, deleteOptions, searchOptions, viewOptions)
                .navSeparatorAdd(pagerSelector, {
                    sepclass: "ui-separator",
                    sepcontent: ""
                })
                .jqGrid('navButtonAdd', pagerSelector, {
                    caption: "",
                    buttonicon: "fa fa-cog",
                    onClickButton: function () {
                        $(gridSelector).jqGrid('columnChooser', {
                            classname: 'col-xs-12',
                            msel: function (selectOptions) {
                                if (selectOptions == "destroy") {
                                    $(this).bootstrapDualListbox("destroy");
                                } else {
                                    //Remove css from select
                                    $(this).removeAttr("style");
                                    $(this).bootstrapDualListbox({
                                        nonSelectedListLabel: '<h4>Hide Columns</h4>',
                                        selectedListLabel: '<h4>Show Columns</h4>',
                                        selectorMinimalHeight: 200
                                    });
                                }
                            },
                            dlog: function (dialogOptions) {
                                if (dialogOptions == "destroy") {
                                    $(this).dialog("destroy");
                                } else {
                                    var defaultDialogOptions = {
                                        title: "<div class='widget-header widget-header-small'><h4 class='smaller'><i class='icon-ok'></i> Choose columns</h4></div>",
                                        autoOpen: false,
                                        height: 500,
                                        width: 500,
                                        modal: true,
                                        create: function () {
                                            //Outer div
                                            $(this).find(">div").addClass("col-xs-12").removeAttr("style");
                                            //Update class for ok button
                                            $(this).closest(".ui-dialog").find(".ui-dialog-buttonset button").eq(0).addClass("btn btn-primary");
                                            //Update class for cancel button
                                            $(this).closest(".ui-dialog").find(".ui-dialog-buttonset button").eq(1).addClass("btn");
                                        }
                                    };

                                    $.extend(dialogOptions, defaultDialogOptions);

                                    $(this).dialog(dialogOptions);
                                    $(this).dialog("open");
                                }
                            },
                            done: function (perm) {
                                // "OK" button are clicked
                                if (perm) {
                                    this.jqGrid("remapColumns", perm, true);
                                    saveGridConfig(gridSelector, gridSettings.Name);

                                }
                                    // we can do some action in case of "Cancel" button clicked
                                else {
                                }
                            }
                        });
                    },
                    position: "last",
                    title: "Choose columns",
                    cursor: "pointer"
                });
            $(gridSelector).jqGrid("showHideColumnMenu", {
                checkboxChecked: "<input class='ace' type='checkbox' checked='checked'><input type='hidden'><label class='lbl'></label>",
                checkboxUnChecked: "<input class='ace' type='checkbox'><input type='hidden'><label class='lbl'></label>",
                checkboxSelector: "input[type=checkbox]",
                toCheck: function ($checkbox, e) {
                    if (e != null && e.originalEvent != null && $(e.originalEvent.target).is("input[type=checkbox]")) {
                        // is the checkbox are clicked then its state is not yet changed
                        return;
                    }
                    $checkbox.prop("checked", true);
                },
                toUnCheck: function ($checkbox, e) {
                    if (e != null && e.originalEvent != null && $(e.originalEvent.target).is("input[type=checkbox]")) {
                        // is the checkbox are clicked then its state is not yet changed
                        return;
                    }
                    $checkbox.prop("checked", false);
                },
                isChecked: function ($checkbox, e) {
                    var checked = $checkbox.is(":checked");
                    if (e != null && e.originalEvent != null && $(e.originalEvent.target).is("input[type=checkbox]")) {
                        // is the checkbox are clicked then its state is not yet changed
                        checked = !checked;
                    }
                    return checked;
                },
                saveConfig: function () {
                    saveGridConfig(gridSelector, gridSettings.Name);
                }
            });
        }

        /*
         ***************** Setup window resize callback 
         */

        $(window).on('resize.jqGrid', function () {
            var parentWidth = $(gridSelector).closest('.tab-pane').width();
            $(gridSelector).jqGrid('setGridWidth', parentWidth - 10);
            jqGridHelper.updateSizeColumnHeaders(gridSelector, true);
        });

        //Resize grid when resize window or access tab
        $(window).on('resize.jqGrid', function () {
            var contentWidth;
            if (isTabGrid != null && isTabGrid) {
                contentWidth = $(gridSelector).closest('.tab-pane').width() - 24;
            } else {
                contentWidth = $(".page-content").width();
            }
            jqGridHelper.resizeGrid(gridSelector, contentWidth);
        });

        $(window).trigger('resize.jqGrid');
    };

    //Set align label
    context.setLabel = function (gridSelector, columnName, className) {
        $(gridSelector).jqGrid("setLabel", columnName, "", className);
    };
    
    // Set tooltip for special header
    context.setTooltipsOnColumnHeader = function(grid, iColumn, text) {
        jQuery("#gbox_" + grid.replace("#", "") + " thead:first tr.ui-jqgrid-labels th:eq(" + iColumn + ") div").attr("title", text);
    };

    //Resize grid base on window size
    context.resizeGrid = function (gridSelector, contentWidth) {
        // Change only for smaller screen than sm
        if (contentWidth < 726) {
            $(gridSelector).jqGrid('setGridWidth', contentWidth, false);
            //Fix the bottom scrollbar issue
            $(gridSelector).closest('div.ui-jqgrid-bdiv').css("width", contentWidth + 1);

            //Sub grid
            $(".sub-grid").jqGrid('setGridWidth', contentWidth, false);
            $(".sub-grid").closest('div.ui-jqgrid-bdiv').css("width", contentWidth + 1);
        } else {
            $(gridSelector).jqGrid('setGridWidth', contentWidth);
            //Fix the bottom scrollbar issue
            $(gridSelector).closest('div.ui-jqgrid-bdiv').css("width", contentWidth + 1);

            //Sub grid
            $(".sub-grid").jqGrid('setGridWidth', contentWidth - 80);
            $(".sub-grid").closest('div.ui-jqgrid-bdiv').css("width", contentWidth - 60);
        }
    };

    //Update grid height
    context.setGridHeightByWindowSize = function (gridSelector, gridHeight) {
        try {
            if (gridHeight < 350) gridHeight = 350;
            $(gridSelector).jqGrid('setGridHeight', gridHeight);
        } catch (e) { }
    };

    // Add dynamic custom-header when load data completed
    context.buildGridHeader = function (gridSelector, pagerSelector) {
        if ($(gridSelector).jqGrid('getGridParam').hideHeader == null || $(gridSelector).jqGrid('getGridParam').hideHeader == false) {
            var idJqgrid = gridSelector.replace('#', '#gbox_');
            // Get button export 
            var htmlGeneralExporButton = '';
            if ($($(idJqgrid).parent().parent()).find('.custom-btn-export').length == 0 && $('.btn-group-export').length > 0) {
                if ($(idJqgrid).parent().parent().length > 0 && $(idJqgrid).parent().parent().hasClass('tab-pane')) {
                    htmlGeneralExporButton = '<div class="grid-buttons btn-group btn-group-export custom-btn-export" style="margin-bottom: -10px;z-index: 100;padding-left:10px;">' + $($(idJqgrid).parent().parent().find('.btn-group-export')).html() + '</div>';
                } else {
                    htmlGeneralExporButton = '<div class="grid-buttons btn-group btn-group-export custom-btn-export" style="margin-bottom: -10px;z-index: 100;padding-left:10px;">' + $('.btn-group-export').html() + '</div>';
                }
            } else {
                // margin-top 27px when grid not have button export
                if ($('.btn-group-export').length == 0) {
                    $(idJqgrid).addClass('margin-top-27');
                }
            }

            // Remove customer header when page reload
            $($(idJqgrid).find('.ui-grid-pager-custom-header')).remove();
            // Get text from bottom
            var records = $($(pagerSelector).find('.ui-paging-info')).text();
            // Add new html to create custom header
            var buttonHtml = '<div class="ui-grid-pager-custom-header ui-pg-button ui-corner-all ui-state-hover" data-placement="left" title="" id="refresh_grid-table-header-custom" data-original-title="Reload Grid"><div class="ui-pg-div"><span class="ui-icon fa fa-refresh green"></span></div></div>';
            $($(idJqgrid).find('.ui-jqgrid-hdiv')).prepend('<div class="ui-grid-pager-custom-header ui-state-default ui-jqgrid-pager ui-corner-top" dir="ltr" style="width: 100%;height: 37px;background-color: #fafafa !important; border: 0 transparent !important;"><div dir="ltr" style="text-align:right;margin-top: 10px;margin-right: 15px;font-weight: 550;" class="ui-paging-info">' + records + buttonHtml + '</div></div>');
            $($(idJqgrid).find('#refresh_grid-table-header-custom')).tooltip();
            $($($(idJqgrid).find('.ui-grid-pager-custom-header')).find('.ui-pg-button')).each(function (i, e) {
                // Register trigger reload page
                $($(e).find('.ui-icon')).click(function () {
                    var currentPostData = $(gridSelector).jqGrid('getGridParam', 'postData');
                    $(gridSelector).jqGrid('setGridParam', { postData: currentPostData });
                    $.ajaxSettings.traditional = true;
                    $(gridSelector).trigger("reloadGrid", [{ page: 1 }]);
                });
                // Register mouse hover for icon
                $(e).hover(function () {
                    $(e).addClass('ui-state-hover');
                }, function () {
                    $(e).removeClass('ui-state-hover');
                });
            });
            $($(idJqgrid).find('.ui-jqgrid-view')).prepend(htmlGeneralExporButton);
        }
    };

    context.hiddenFooter = function (gridSelector) {
        // Check option setting of gird to hide or show footer
        if ($(gridSelector).jqGrid('getGridParam').hideFooter != null || $(gridSelector).jqGrid('getGridParam').hideFooter == true) {
            var idJqgrid = gridSelector.replace('#', '#gbox_');
            $($(idJqgrid).find('.ui-corner-bottom')).hide();
            $($(idJqgrid).find('.ui-jqgrid-bdiv')).css('min-height', '0');
            $($(idJqgrid).find('.ui-jqgrid-bdiv')).css('border-bottom', 'none');
        }
    };

    // Convert string to pixel
    context.convertStrToPixel = function (str) {
        var canvas = document.createElement('canvas');
        var ctx = canvas.getContext("2d");
        ctx.font = "14px Arial";
        return ctx.measureText(str).width;
    };

    // Make email as mailto link
    context.MailToLink = function (email) {
        // Check if text is email
        if (siteHelper.isEmail(email)) {
            return "<a href='mailto:" + email + "' data-rel='tooltip' data-title='" + email + "'>" + email + "</a>";
        }
        return email;
    };

})(jqGridHelper);

// Trigger event resize jqgrid when menu sider-bar expand or collapse
$('#sidebar-collapse').on('click', function () {
    setTimeout(function () {
        $(window).trigger('resize.jqGrid');
    });
});

//Expand json value
function expandJsonValue(selector, element, text) {
    $(element).toggle();
    if ($(element).css('display') == 'none') {
        $(selector).html("Click to show " + text);
    }
    else {
        $(selector).html("Click to hide " + text);
    }
}

/*
 * Save grid config
 */
var saveGridConfig = function (gridSelector, gridName) {
    $(window).trigger('resize.jqGrid');

    var colModels = $(gridSelector).jqGrid('getGridParam', 'colModel');
    var order = $(gridSelector).jqGrid('getGridParam', 'remapColumns');

    var data = {
        name: gridName,
        Columns: [],
        Order: order
    };

    if (colModels.length > 0) {
        $.each(colModels, function (index, value) {
            if (value != undefined) {
                data.Columns.push({
                    Name: value.name,
                    Show: !value.hidden,
                });
            }
        });
    }

    siteHelper.httpPost({
        showLoading: false,
        url: "/Account/SaveCurrentUserGridConfig",
        data: data,
        success: function (response) {
            siteHelper.showMessage(response);
            jqGridHelper.updateSizeColumnHeaders(gridSelector);
        }
    });
};

/*
 * Get column index
 */
var getColumnIndex = function (grid, columnIndex) {
    var cm = grid.jqGrid('getGridParam', 'colModel'), i = 0, l = cm.length;
    for (; i < l; i += 1) {
        if (cm[i] != undefined && (cm[i].index || cm[i].name) === columnIndex) {
            return i; // return the colModel index
        }
    }
    return -1;
};

/*
 * Sync jqGrid filterToolbar and searchGrid filters
 * http://stackoverflow.com/questions/6875294/two-related-questions-on-jqgrid-column-header-filters-and-the-advanced-filtering/6884755#6884755
 */
var refreshSearchingToolbar = function ($grid, myDefaultSearch) {
    var postData = $grid.jqGrid('getGridParam', 'postData'), filters, i, l,
        rules, rule, iCol, cm = $grid.jqGrid('getGridParam', 'colModel'),
        cmi, control, tagName;

    for (i = 0, l = cm.length; i < l; i += 1) {
        var name = cm[i] != undefined ? cm[i].name : "";
        control = $("#gs_" + $.jgrid.jqID(name));
        if (control.length > 0) {
            tagName = control[0].tagName.toUpperCase();
            if (tagName === "SELECT") { // && cmi.stype === "select"
                control.find("option[value='']")
                    .attr('selected', 'selected');
            } else if (tagName === "INPUT") {
                control.val('');
            }
        }
    }

    if (typeof (postData.filters) === "string" &&
            typeof ($grid[0].ftoolbar) === "boolean" && $grid[0].ftoolbar) {

        try {
            filters = $.parseJSON(postData.filters);
        } catch (e) { }
        if (filters && filters.groupOp === "AND" && typeof (filters.groups) === "undefined") {
            // only in case of advance searching without grouping we import filters in the
            // searching toolbar
            rules = filters.rules;
            for (i = 0, l = rules.length; i < l; i += 1) {
                rule = rules[i];
                iCol = getColumnIndex($grid, rule.field);
                cmi = cm[iCol];
                if (cmi != null && cmi != undefined) {
                    control = $("#gs_" + $.jgrid.jqID(cmi.name));
                    if (iCol >= 0 && control.length > 0) {
                        tagName = control[0].tagName.toUpperCase();
                        if (((typeof (cmi.searchoptions) === "undefined" ||
                              typeof (cmi.searchoptions.sopt) === "undefined")
                             && rule.op === myDefaultSearch) ||
                                (typeof (cmi.searchoptions) === "object" &&
                                    $.isArray(cmi.searchoptions.sopt) &&
                                    cmi.searchoptions.sopt[0] === rule.op)) {

                            if (tagName === "SELECT") { // && cmi.stype === "select"
                                control.find("option[value='" + $.jgrid.jqID(rule.data) + "']")
                                    .attr('selected', 'selected');
                            } else if (tagName === "INPUT") {
                                control.val(rule.data);
                            }
                        }
                    }
                }
            }
        }
    }
};