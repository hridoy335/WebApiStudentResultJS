$(document).ready(function() { 
    getStudentDropdown();
    getExamDropdown();
    GetCourseDropdown();

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

function GetCourseDropdown() {
    $.ajax({
        type: "GET",
        url: "../api/Courses",
        success: function (data) {
            var cou = $('#course');
            $.each(data, function (k, v) {
                cou.append('<option value="' + v.CourseId + '">' + v.CourseName + '</option>');
            });
        }
    })
}

function AddRow() {
   
}

jQuery(document).delegate('.add-record', 'click', function (e) {
    e.preventDefault();
    var courseValue = $('#course').val()
    var marksValue = $('#marks').val();
    if (courseValue == null || typeof (courseValue) == 'undefined' || courseValue == '') {
        alert('Please Select Course');
        return;
    }
    var courseText = jQuery("#course option:selected").text();
    var invalidCourse = false;
    $('#resultTable tr').each(function () {
        var courseId = $(this).find(".course").html();
        if (courseText == courseId) {
            invalidCourse = true;
            return;
        }
    });
    if (invalidCourse === true) {
        alert('Course Aleady Added to Result Sheet.');
        return;
    }
    

    if (marksValue == null || typeof (marksValue) == 'undefined' || marksValue=='' ) {
        alert('Please Insert Marks');
        return;
    }
    if (marksValue > 100 || marksValue<=0) {
        alert('Invalid Marks! Please Enter a valid Mark!');
        $('#marks').val('');
        return;
    }

    var grade = null;
    var gradeValue = marksValue / 10;
    if (gradeValue>= 9) {
        grade = 'A';
    } else if (gradeValue >= 8 && gradeValue < 9) {
        grade = 'B'
    } else if (gradeValue >= 7 && gradeValue < 8) {
        grade = 'C'
    } else if (gradeValue >= 6 && gradeValue < 7) {
        grade = 'D'
    } else {
        grade = 'F'
    }
    if (grade == null) {
        alert('Something Went Wrong!!!!')
    }

    var content = jQuery('#sample_table tr'),
        size = jQuery('#resultTable >tbody >tr').length - 1,
        element = null,
        element = content.clone();
    element.attr('id', 'rec-' + size);
    element.find('.delete-record').attr('data-id', size);
    element.appendTo('#resultTable_body');
    element.find('.sn').html(size);
    element.find('.course').html(courseText);
    element.find('.marks').html(marksValue);
    element.find('.grade').html(grade);
    $('#course').val('');
    $('#marks').val('');
    var total = calculateTotal();
    $('#total').html(total);
    //$("#course option[value='" + courseValue +"']").remove();
    
});

jQuery(document).delegate('.delete-record', 'click', function (e) {
    e.preventDefault();
    var didConfirm = confirm("Are you sure You want to delete");
    if (didConfirm == true) {
        var id = jQuery(this).attr('data-id');
        var targetDiv = jQuery(this).attr('targetDiv');
        jQuery('#rec-' + id).remove();

        //regnerate index number on table
        $('#tbl_posts_body tr').each(function (index) {
            //alert(index);
            $(this).find('span.sn').html(index + 1);
        });
        var total = calculateTotal();
        $('#total').empty();
        $('#total').html(total);
        return true;
    } else {
        return false;
    }
   
});

jQuery(document).delegate('.edit-record', 'click', function (e) {
    var element = $(this).parents('tr');//.attr('id');
    var course = element.find('.course').html();
    var marks = element.find('.marks').html();
    /*$("#course option:contains(" + course + ")").attr('selected', 'selected');*/
    $('#marks').val(marks);
/*    $('#course').find("select option:contains('" + course + "')").attr('selected',true)*/
});

function calculateTotal() {
    var total = 0;
    $('#resultTable tr').each(function () {
        var marks = $(this).find(".marks").html();
        if (marks !== null && typeof (marks) !== 'undefined') {
            total += parseInt(marks);
        }
        
    });
    return total;
}