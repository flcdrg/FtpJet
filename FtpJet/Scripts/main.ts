declare var base: string;

$(() => {
    $('#Search').on('click', (eventObject: JQueryEventObject) => {
        $.getJSON(base + 'api/flights')
            .done((d: JQueryPromiseCallback<any>) => {
            console.log(d);
        });
    });
});
