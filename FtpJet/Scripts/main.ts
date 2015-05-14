declare var base: string;

$(() => {
    $('#Search').on('click', (eventObject: JQueryEventObject) => {
        $.getJSON(base + 'api/flights')
            .done((d: any[]): void => {
            
            var start = moment().tz(d[0].start);
            console.log(start.format());
            var finish = moment(d[0].finish);
            console.log(finish.format());
        });
    });
});
