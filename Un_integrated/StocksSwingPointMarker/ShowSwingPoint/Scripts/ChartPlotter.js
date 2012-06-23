function PlotCandleStickChart(containerId, data) {
    $.jqplot(containerId, [data], {
        // use the y2 axis on the right of the plot
        //rather than the y axis on the left.
        seriesDefaults: { yaxis: 'y2axis' },
        axes: {
            xaxis: {
                renderer: $.jqplot.DateAxisRenderer,
                tickOptions: { formatString: '%b %e' },
                // For date axes, we can specify ticks options as human
                // readable dates.  You should be as specific as possible,
                // however, and include a date and time since some
                // browser treat dates without a time as UTC and some
                // treat dates without time as local time.
                // Generally, if  a time is specified without a time zone,
                // the browser assumes the time zone of the client.
                min: "06-01-2011",
                max: "06-30-2012",
                tickInterval: "4 weeks"
            },
            y2axis: {
                tickOptions: { formatString: 'Rs.%d' }
            }
        },
        series: [
            { renderer: $.jqplot.OHLCRenderer,
              rendererOptions: { candleStick: true,
                  fillUpBody: true,
                  upBodyColor: '#00ff00',
                  fillDownBody: true,
                  downBodyColor: '#ff0000'
              },
              pointLabels: { show: true, location: 'n', ypadding: 5}
            }
        ],
        highlighter: {
            show: true,
            showMarker: true,
            tooltipAxes: 'xy',
            yvalues: 5,
            tooltipOffset: 10,
            // You can customize the tooltip format string of the highlighter
            // to include any arbitrary text or html and use format string
            // placeholders (%s here) to represent x and y values.
            formatString: '<table class="jqplot-highlighter"> \
      <tr><td>Date:</td><td>%s</td></tr> \
      <tr><td>Open:</td><td>%s</td></tr> \
      <tr><td>Hi:</td><td>%s</td></tr> \
      <tr><td>Low:</td><td>%s</td></tr> \
      <tr><td>Close:</td><td>%s</td></tr> \
      <tr><td>Volume:</td><td>%s</td></tr></table>'
        }
    });
}