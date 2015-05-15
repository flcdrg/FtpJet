declare var base: string;

var myViewModel = {
    flights: ko.observableArray()
};

$(() => {
    $('#Search').on('click', (eventObject: JQueryEventObject) => {
        $.getJSON(base + 'api/flights')
            .done((data: {code: string, start: string, finish: string}[]): void => {

            data.forEach((row) => {
                var [date, timezone] = row.start.split(' ');

                var start = moment(date).tz(timezone);

                [date, timezone] = row.finish.split(' ');

                var finish = moment(date).tz(timezone);

                myViewModel.flights.push({
                    code: row.code,
                    departure: start.format(),
                    arrival: finish.format()
                });
            });
        });
    });

    ko.applyBindings(myViewModel);
});


