$(document).ready(function() { 
    getStudentDropdown();
    getExamDropdown();

});

function getStudentDropdown() {
    $.ajax({
        type: "GET",
        url: "../api/Students",
        success: function (stdData) {
            /*var studentR = document.getElementById('student');
            for (var i = 0; i <= stdData.length; i++) {
                studentR.innerHTML += '<option value = "' + stdData[0].StudentId + '">' + stdData[0].StudentName + '</option>'; 
            }
            <option value= "1"> Noni </option>*/

            var student = $('#student');
            $.each(stdData, function (k, v) {
                student.append('<option value="' + v.StudentId + '">' + v.StudentName + '</option>');
            });
        }
    });


}

function getExamDropdown() {
    /*var exname = $('#kudfh').val();
    $.ajax({
        type: 'POST',
        url: 'asdug',
        data: {
            ExamName: exname
        },
    })*/
    $.ajax({
        type: "GET",
        url: "../api/ExamTypes",
        success: function (Exam) {
            var sep1 = $('#examType');
            $.each(Exam, function (k, v) {
                sep1.append('<option value="' + v.ExamTypeId +'">' + v.ExamName+'</option>');
            });
        }

    })
}

function AddRow() {
    alert('Clicked');
}
