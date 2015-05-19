declare var base: string;

var myViewModel = {
    flights: ko.observableArray()
};

function loadFlights(startDate?: string) {
    var url = base + 'api/flights';

    if (startDate != null) {
        url += '?startDate=' + startDate;
    }

    $.getJSON(url)
        .done((data: { code: string, source: string, destination: string, start: string, finish: string, duration: string }[]): void => {

        data.forEach((row) => {
            var [date, timezone] = row.start.split(' ');

            var start = moment(date).tz(timezone);

            [date, timezone] = row.finish.split(' ');

            var finish = moment(date).tz(timezone);

            myViewModel.flights.push({
                code: row.code,
                source: row.source,
                destination: row.destination,
                departure: start.format(),
                departureInfo: `DST: ${start.isDST() }`,
                arrival: finish.format(),
                arrivalInfo: `DST: ${finish.isDST() }`,
                duration: row.duration
            });
        });
    });
}

$(() => {
    var startDate = moment().format('YYYY-MM-DD');
    $('#StartDate').val(startDate);

    $('#Search').on('click', (eventObject: JQueryEventObject) => {
        var startDate = $('#StartDate').val();

        loadFlights(startDate);
    });

    ko.applyBindings(myViewModel);

    loadFlights(startDate);
});


