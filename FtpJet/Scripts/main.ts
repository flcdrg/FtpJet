declare var base: string;

$(() => {
    $('#Search').on('click', (eventObject: JQueryEventObject) => {
        $.getJSON(base + 'api/flights')
            .done((d: string): void => {
            d
            console.log(d);
        });
    });
});
