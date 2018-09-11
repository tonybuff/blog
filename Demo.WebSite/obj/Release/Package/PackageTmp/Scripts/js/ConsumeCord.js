var ConsumeCord= {
    Page: function (index) {
        var _consumeType = $('#type').val();
        $.post('/home/_ConsumeRecord', { consumeType: _consumeType, page: index }, function(data) {
                $('#demo1').html(data);
        });
    },
    SubmitAdd: function() {
        var _consumeType = $('#consumeType').val();
        var _money = $('#money').val();
        var model = {
            ConsumeType: _consumeType,
            Money: _money
        };
        $.post('/home/SaveConsumeRecord', model, function(data) {
            if (data.msg == '新增成功') {
                window.location.reload();
            } else {
                alert(data.msg);
            }
        });
    }
}
$(function() {
    var lineChartData = {
        labels: ["菜", "米", "you", "", "", "", ""],
        datasets: [
            {
                fillColor: "rgba(220,220,220,0.5)",
                strokeColor: "rgba(220,220,220,1)",
                pointColor: "rgba(220,220,220,1)",
                pointStrokeColor: "#fff",
                data: [65, 59, 90, 81, 56, 55, 40]
            },
            {
                fillColor: "rgba(151,187,205,0.5)",
                strokeColor: "rgba(151,187,205,1)",
                pointColor: "rgba(151,187,205,1)",
                pointStrokeColor: "#fff",
                data: [28, 48, 40, 19, 96, 27, 100]
            }
        ]

    };
    new Chart(document.getElementById("line").getContext("2d")).Line(lineChartData);
})