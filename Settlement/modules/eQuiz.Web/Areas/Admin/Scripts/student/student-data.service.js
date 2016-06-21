(function (angular) {

    angular
        .module("settlementModule")
        .factory("studentDataService", studentDataService);

    studentDataService.$inject = ["$http"];

    function studentDataService($http) {

        var service = {
            getStudentInfo: getStudentInfo,
            saveProfileInfo: saveProfileInfo
        };

        return service;

        //function getStudentInfo(studentId) {
        //    var promise = $http({
        //        url: '/Admin/Student/GetStudentInfo',
        //        method: "GET",
        //        params: { id: studentId }
        //    });
        //    return promise;
        //}
        function getStudentInfo(studentId) {
            return {
                firstName : 'John',
                lastName : 'Smith',
                institute: 'IKTA',
                studyGroup: 'ZI-31',
                paymentTill: '15.08.2017',
                paidSum: 200,
                hostel: 8,
                room: 217
            }
        }

        function saveProfileInfo(id, firstName, lastName, phone) {
            var promise = $http({
                url: '/Admin/Student/UpdateUserInfo',
                method: "POST",
                params: { id: id, firstName: firstName, lastName: lastName, phone: phone }
            });
            //var promise = $http.post("/Admin/Student/UpdateUserInfo", { Id: id, firstName: firstName, lastName: lastName, phone: phone });
            return promise;
        }
    }

})(angular);