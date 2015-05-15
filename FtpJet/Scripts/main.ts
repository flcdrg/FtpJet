declare var base: string;

$(() => {
    $('#Search').on('click', (eventObject: JQueryEventObject) => {
        $.getJSON(base + 'api/flights')
            .done((data: {code: string, start: string, finish: string}[]): void => {

            data.forEach((row) => {
                var [date, timezone] = row.start.split(' ');

                var start = moment(date).tz(timezone);

                [date, timezone] = row.finish.split(' ');

                var finish = moment(date).tz(timezone);
                $('#flights').append(`<tr><td>${row.code}</td><td>${start}</td><td>${finish}</td></tr>`);

            });
        });
    });
});
