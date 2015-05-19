declare var base: string;

var myViewModel = {
    flights: ko.observableArray()
};

function loadFlights(startDate: string, duration: number) {
    var url = base + 'api/flights';

    url += `?startDate=${startDate}&duration=${duration}`;

    $.getJSON(url)
        .done((data: { code: string, source: string, destination: string, start: string, finish: string, duration: string }[]): void => {

        data.forEach((row) => {
            var [date, timezone] = row.start.split(' ', 2);

            var start = moment.tz(date, timezone);

            var [date2, timezone2] = row.finish.split(' ', 2);

            var finish = moment.tz(date2, timezone2);

            var existing = myViewModel.flights().filter((value: any) => { return value.code == row.code });

            if (existing.length == 0) {

                myViewModel.flights.push({
                    code: row.code,
                    source: row.source,
                    destination: row.destination,
                    departure: ko.observable(start.format()),
                    departureInfo: ko.observable(`DST: ${start.isDST() }`),
                    arrival: ko.observable(finish.format()),
                    arrivalInfo: ko.observable(`DST: ${finish.isDST() }`),
                    duration: ko.observable(row.duration)
                });
            } else {
                existing.forEach((value: any) => {
                    value.departure(start.format());
                    value.departureInfo(`DST: ${start.isDST() }`);
                    value.arrival(finish.format());
                    value.arrivalInfo(`DST: ${finish.isDST() }`);
                    value.duration(row.duration);
                });
            }
        });
    });
}

$(() => {
    var startDate = moment().format('YYYY-MM-DD');
    $('#StartDate').prepend(`<option selected='true'>${startDate}</option>`);

    $('#Search').on('click', (eventObject: JQueryEventObject) => {
        var startDate = $('#StartDate').val();

        
        loadFlights(startDate, $('#Duration').val());
    });

    ko.applyBindings(myViewModel);

    loadFlights(startDate, $('#Duration').val());
});


