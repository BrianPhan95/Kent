var todayDateRange = todayDateRange || 0;
var getSubDomainSummaryUrl = getSubDomainSummaryUrl || "";

/***************************************** Document Ready Function ******************************************/
$(document).ready(function () {
    //////////////////////// iBox
    // Collapse ibox function
    $('.collapse-link').on('click', function () {
        var ibox = $(this).closest('div.ibox');
        var button = $(this).find('i');
        var content = ibox.find('div.ibox-content');
        content.slideToggle(100);
        button.toggleClass('fa-chevron-up').toggleClass('fa-chevron-down');
        ibox.toggleClass('').toggleClass('border-bottom');
    });

    // Close ibox function
    $('.close-link').on('click', function () {
        var content = $(this).closest('div.ibox');
        content.remove();
    });

    // Full screen chart function
    $('.btn-maximize-chart').on('click', function () {
        var chart = $(this).closest('div.pos-relative');
        var button = $(this).find('i');
        $('body').toggleClass('fullscreen-ibox-mode');
        button.toggleClass('fa-expand').toggleClass('fa-compress');
        chart.toggleClass('fullscreen');
        if (chart.hasClass("fullscreen")) {
            $(this).attr("data-original-title", "Minimize");
        } else {
            $(this).attr("data-original-title", "Maximize");
        }
        setTimeout(function () {
            $(window).trigger('resize');
        }, 100);
    });

    ////////////////////// Date pickers
    siteHelper.buildDateRange("#subdomain-date-from", "#subdomain-date-to");
    // Keep drop-down opening when clicking on custom element
    $("#subdomain-custom-date-range").click(function (e) {
        e.stopPropagation();
    });
    
    
    /*********************** Google Chart first load ****************************/
    // Load the Visualization API and the core chart package.
    google.charts.load('current', { 'packages': ['corechart', 'table'] });
    // Callback to run when the Google Visualization API is loaded.
    google.charts.setOnLoadCallback(
    function () {
        // Sub Domain Summary
        renderSubDomainSummary();
    });

});

/********************************************** Helper Functions **********************************************/
/*
 * Render the sub domain statistics
 */
function renderSubDomainSummary() {

    // Create the data table for both charts
    var chartData = new google.visualization.DataTable();
    chartData.addColumn('string', 'Sub Domain');
    chartData.addColumn('number', "Leads");
    chartData.addColumn('number', "List Views");
    chartData.addColumn('number', "Detail Views");
    chartData.addColumn('number', "Www Clicks");
    chartData.addColumn('number', "Email Clicks");
    chartData.addColumn('number', "Phone Clicks");

    // Set column chart options
    var colChartOptions = {
        title: "* Click on chart to show more detail",
        titlePosition: 'out',
        titleTextStyle: {
            color: '#ed5565',
            fontSize: 10,
            bold: false,
            italic: true
        },
        series: {
            0: { color: '#3366cc' },
            1: { color: '#109618' },
            2: { color: '#3dc7ab' },
            3: { color: '#dc3912' },
            4: { color: '#ff9900' },
            5: { color: '#990099' },
        },
        hAxis: {
            title: '',
            textStyle: {
                fontSize: 10,
                bold: true
            },
            slantedText: true,
            slantedTextAngle: 45,
            maxTextLines: 1,
            viewWindow: {
                min: 0
            }
        },
        vAxis: {
            format: '#',
            textStyle: {
                fontSize: 11
            }
        },
        legend: {
            position: 'top',
            alignment: 'start',
            textStyle: { fontSize: 11 }
        },
        bar: {
            groupWidth: '85%'
        },
        //        isStacked: true,
        chartArea: {
//            left: 45,
            width: '90%',
//            top: 25,
//            height: '70%'
        },
        animation: {
            duration: 1000,
            easing: 'out',
            startup: true
        }
    };

    // Set table chart options
    var tabChartOptions = {
        showRowNumber: false,
        sort: 'enable',
        startPage: 0,
        page: 'enable',
        pageSize: 50,
        width: '100%',
        height: '100%',
        allowHtml: true,
        frozenColumns: 1
    };
    // Instantiate the column chart
    var colChart = new google.visualization.ColumnChart(
            document.getElementById('subdomain-chart'));


    // Instantiate the table chart
    var tabChart = new google.visualization.Table(
        document.getElementById('subdomain-table-chart'));

    // Used to display resize buttons or not?
    var maxColChartColNum = 10;  // Const
    var lowerLimit, upperLimit;
    var isZoom = false;
    var subDomainNum;
    
    var chartNum;
    var data;

    var dr, cdf, cdt;

    function displayButtons() {
        $("#maximize-subdomain-btn").css("display", "inline-block");
        $("#maximize-subdomain-btn").prop("disabled", false);

        $("#subdomain-summary-export-btn").css("display", "inline-block");
        $("#subdomain-summary-export-btn").prop("disabled", false);

        $("#subdomain-date-ranges-btn").prop("disabled", false);
        
        if (maxColChartColNum < subDomainNum) {
            $("#subdomain-chart-btn .btn-resize-chart").css("display", "inline-block");
            $("#subdomain-chart-btn .btn-resize-chart").prop("disabled", false);
            if (lowerLimit == 0 || isZoom) {
                $("#subdomain-chart-btn .btn-resize-chart[data-role='previous']").prop("disabled", true);
            }
            if (upperLimit >= subDomainNum || isZoom) {
                $("#subdomain-chart-btn .btn-resize-chart[data-role='next']").prop("disabled", true);
            }
        } else {
            $("#subdomain-chart-btn .btn-resize-chart").css("display", "none");
            $("#subdomain-chart-btn .btn-resize-chart").prop("disabled", true);
        }
    }

    function disableButtons() {
        $("#maximize-subdomain-btn").prop("disabled", true);
        $("#subdomain-summary-export-btn").prop("disabled", true);
        $("#subdomain-date-ranges-btn").prop("disabled", true);
        $("#subdomain-chart-btn .btn-resize-chart").prop("disabled", true);
    }

    function thrillDownSubdomain(selections) {
        var sel = selections[0];
        
        // Click on the legend
        if (sel == undefined || sel.row == null)
            return;

        var dealerDomain = data.SubDomains[sel.row].DealerDomain;
        window.location.href = "/admin/vehicledashboard/stocks?sd=" + dealerDomain + "&dr=" + dr + "&cdf=" + cdf +"&cdt=" + cdt ;
    }

    /*
     * Draw the column chart
     */
    function drawColChart(addListener) {
        if (addListener != undefined && addListener == true) {
            google.visualization.events.addListener(colChart, 'ready',
                function() {
                    chartNum--;
                    if (chartNum <= 0) {
                        displayButtons();
                    }
                });

            // Event trigger when click on chart column
            google.visualization.events.addListener(colChart, 'select',
                function() {
                    thrillDownSubdomain(colChart.getSelection());
                });
        }

        // And draw chart
        colChart.draw(chartData, colChartOptions);
    }

    /*
     * Draw the table chart
     */
    function drawTabChart(addListener) {
        if (addListener != undefined && addListener == true) {
            google.visualization.events.addListener(tabChart, 'ready',
                function() {
                    chartNum--;
                    if (chartNum <= 0) {
                        displayButtons();
                    }
                });
            
            // Event trigger when click on a row of table chart
            google.visualization.events.addListener(tabChart, 'select',
                function () {
                    thrillDownSubdomain(tabChart.getSelection());
                });
        }

        // And draw chart
        tabChart.draw(chartData, tabChartOptions);
    }

    /*
     * Get data for charts from server
     */
    function getSubdomainSummary(dateRangeType, customDateFrom, customDateTo, isFirstTime) {
        chartNum = 2;
        disableButtons();

        Pace.stop();
        Pace.track(function () {
            siteHelper.httpPost({
                showLoading: false,
                url: getSubDomainSummaryUrl,
                data: {
                    dateRangeType: dateRangeType,
                    customDateFrom: customDateFrom,
                    customDateTo: customDateTo
                },
                success: function (response) {
                    if (response.Success) {
                        data = response.Data;
                        var timeSpan = data.TimeSpan;
                        var subDomainData = data.SubDomains;
                        subDomainNum = subDomainData.length;
                        
                       var rowNum = chartData.getNumberOfRows();
                        if (rowNum > 0) {
                            chartData.removeRows(0, rowNum);
                        }
                        
                        for (var i = 0; i < subDomainNum; i++) {
                            var row = subDomainData[i];
                            chartData.addRow([
                                row.DealerDomain,
                                row.Leads,
                                row.ListViews,
                                row.DetailViews,
                                row.WwwClicks,
                                row.EmailClicks,
                                row.PhoneClicks
                            ]);
                        }
                        
                        // Update date range button label
                        if (timeSpan != null && timeSpan != "") {
                            $("#subdomain-date-ranges-btn span.time-span").text(": " + timeSpan);;
                        }

                        // Draw charts
                        lowerLimit = 0;
                        upperLimit = subDomainNum <= maxColChartColNum
                            ? subDomainNum
                            : maxColChartColNum;
                        isZoom = false;
                        colChartOptions.hAxis.viewWindow.min = 0;
                        colChartOptions.hAxis.viewWindow.max = upperLimit;
                        
                        if (isFirstTime != undefined && isFirstTime == true) {
                            drawColChart(true);
                            drawTabChart(true);
                        } else {
                            drawColChart();
                            drawTabChart();
                        }
                    }
                }
            });
        });
    }

    // First load
    var defaultDaterange = $("#subdomain-btn-group-date-range .date-range-selected")[0];
    dr = defaultDaterange != undefined
        ? $(defaultDaterange).data("value")
        : todayDateRange;
    cdf = $("#subdomain-date-from").val();
    cdt = $("#subdomain-date-to").val();
    
    getSubdomainSummary(dr, cdf, cdt, true);

    // When window is resized (responsive)
    $(window).on('resize', function () {
        chartNum = 1;
        disableButtons();
        drawColChart();
    });

    // Change date range type
    $("#subdomain-btn-group-date-range .date-range").click(function () {
        var el = $(this);
        // Change button label
        $("#subdomain-date-ranges-btn span.btn-label").text(el.text());
        $("#subdomain-date-ranges-btn span.time-span").text('');

        dr = el.data("value");

        $("#subdomain-btn-group-date-range .date-range-selected")
            .removeClass("date-range-selected").addClass("date-range-unselected");
        el.removeClass("date-range-unselected").addClass("date-range-selected");

        getSubdomainSummary(dr, cdf, cdt);
    });

    // Custom date range
    $("#subdomain-custom-date-range-btn").click(function () {
        dr = $(this).val();
        cdf = $("#subdomain-date-from").val();
        cdt = $("#subdomain-date-to").val();

        $("#subdomain-date-ranges-btn span.btn-label").text($("#subdomain-custom-date-range .custom-title").text());
        $("#subdomain-date-ranges-btn span.time-span").text('');

        $("#subdomain-btn-group-date-range .date-range-selected")
            .removeClass("date-range-selected").addClass("date-range-unselected");
        $("#subdomain-custom-date-range .date-range-unselected").removeClass("date-range-unselected")
            .addClass("date-range-selected");
        $("#subdomain-date-ranges-btn").dropdown("toggle");

        getSubdomainSummary(dr, cdf, cdt);
    });
    
    // Resize the chart
    $("#subdomain-chart-btn .btn-resize-chart").click(function () {
        var role = $(this).data('role');

        if (role == "previous") {
            if (maxColChartColNum <= subDomainNum) {
                if (lowerLimit > 0) {
                    lowerLimit -= maxColChartColNum;
                    upperLimit -= maxColChartColNum;
                }
            } else {
                lowerLimit = 0;
                upperLimit = subDomainNum;
            }
        } else if (role == "next") {
            if (maxColChartColNum <= subDomainNum) {
                if (upperLimit < subDomainNum) {
                    lowerLimit += maxColChartColNum;
                    upperLimit += maxColChartColNum;
                }
            } else {
                lowerLimit = 0;
                upperLimit = subDomainNum;
            }
        } else {
            // Role == "zoom"
            isZoom = !isZoom;
        }
        colChartOptions.hAxis.viewWindow.min = isZoom ? 0 : lowerLimit;
        colChartOptions.hAxis.viewWindow.max = isZoom ? subDomainNum : upperLimit;

        chartNum = 1;
        disableButtons();

        drawColChart();
    });

    // Export debtors courses summary data
    $('#subdomain-summary-export-btn').click(function () {
        var subDomainData = data.SubDomains;
        var columns = [
            { name: "DealerDomain", display: "Dealer Domain" },
            "Leads",
            { name: "ListViews", display: "List Views" },
            { name: "DetailViews", display: "Detail Views" },
            { name: "WwwClicks", display: "Www Clicks" },
            { name: "EmailClicks", display: "Email Clicks" },
            { name: "PhoneClicks", display: "Phone Clicks" }];
        siteHelper.exportJsonToCsv(subDomainData, "Vehicle_SubDomain_Summary", columns);
    });
}