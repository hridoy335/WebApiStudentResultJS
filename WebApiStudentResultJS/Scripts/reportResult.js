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

////function AddRow() {
   
////}

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


    if (marksValue == null || typeof (marksValue) == 'undefined' || marksValue == '') {
        alert('Please Insert Marks');
        return;
    }
    if (marksValue > 100 || marksValue <= 0) {
        alert('Invalid Marks! Please Enter a valid Mark!');
        $('#marks').val('');
        return;
    }

    var grade = null;
    var gradeValue = marksValue / 10;
    if (gradeValue >= 9) {
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
    element.find('.courseID').html(courseValue);
    element.find('.marks').html(marksValue);
    element.find('.grade').html(grade);
    $('#course').val('');
    $('#marks').val('');
    var total = calculateTotal();
    var sgpa = calculateSGPA();
    $('#total').html(total);
    $('#total2').html(sgpa);
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
        var sgpa = calculateSGPA();
        $('#total').empty();
        $('#total').html(total);
        $('#total2').html(sgpa);
        return true;
    } else {
        return false;
    }
   
});

jQuery(document).delegate('.edit-record', 'click', function (e) {
    var element = $(this).parents('tr');
    var id = element.attr('data-id');
    var course = element.find('.course').html();
    var courseID = element.find('.courseID').html();
    var marks = element.find('.marks').html();
    /*$("#course option:contains(" + course + ")").attr('selected', 'selected');*/
    $('#marks').val(marks);
    $('#course').val(courseID);
    element.remove();
    var total = calculateTotal();
    var sgpa = calculateSGPA();
    $('#total').empty();
    $('#total').html(total);
    $('#total2').html(sgpa);
    //$('#course').find("select option:contains('" + courseID + "')").attr('selected', true);
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

function calculateSGPA() {
    var totalsgpa = 0;
    var rowCount = 0;
    $('#resultTable tr').each(function () {
        var grade = $(this).find(".grade").html();
        if (grade == 'A') {
            totalsgpa += 4;
            rowCount += 1;
        }
        else if (grade == 'B') {
            totalsgpa += 3;
            rowCount += 1;
        }
        else if (grade == 'C') {
            totalsgpa += 2;
            rowCount += 1;
        }
        else if (grade == 'D') {
            totalsgpa += 1;
            rowCount += 1;
        }
        else if (grade == 'F') {
            totalsgpa += 0;
            rowCount += 1;
        }
    });
    if (rowCount > 0) {
        totalsgpa /= rowCount;
    }
    
    if (typeof (totalsgpa) == 'undefined') {
        totalsgpa = 0;
    }
    return totalsgpa.toFixed(2);



}

function saveData() {

    var student = $("#student").val();
 //   alert(student);
    var examType = $("#examType").val();
  //  alert(examType);
  
    var rowCount = 0;
    $('#resultTable tr').each(function () {
        var marks = $(this).find(".marks").html();
        if (marks !== null && typeof (marks) !== 'undefined') {
            rowCount += 1;
        }
    });
        

    if (student == null || typeof (student) == 'undefined' || student == '') {
        alert('Please select student..');
        return;
    }
     if (examType == null || typeof (examType) == 'undefined' || examType == '') {
         alert('Please select examtype..');
         return;
    }
    if (rowCount == 0) {
        alert('Data Grid is empty.. ');
        return;
    }
    var reportResultList = [];
    $('#resultTable tr').each(function () {
        var courseid = $(this).find(".courseID").html();
        var marks = $(this).find(".marks").html();
        if (marks !== null && typeof (marks) !== 'undefined') {
            var data = {
                //ReportResultId: 1,
                StudentId: student,
                CourseId: courseid,
                ExamTypeId: examType,
                Marks: marks
            }
            //reportResultList.push(data);
            $.ajax({
                type: "POST",
                url: "../api/ReportResults1",
                data: data,
                success: function (data) {
                        alert("Data saved successfully");
                }
            })
        }
        
    });
   /* console.log(JSON.stringify(reportResultList))
    $.ajax({
        type: "POST",
        url: "../api/ReportResults1",
        data: reportResultList,
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            if (data.length > 0) {
                alert("Data saved successfully");
            } else {
                alert("Something went wrong! please try again!");
            }
            
        }
    })
   */ 
    

    

/*
    $.ajax({
        type: "POST",
        url: "../api/ReportResults1",
        data: data
    })*/

}
