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
        var _user = $('#users').val();
        var model = {
            ConsumeType: _consumeType,
            Money: _money,
            User:_user
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
   
    new Chart(document.getElementById("line").getContext("2d")).Line(lineChartData);
})