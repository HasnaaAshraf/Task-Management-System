document.addEventListener("click", function (e) {

    if (e.target.classList.contains("delete-btn")) {

        const button = e.target;
        const form = button.closest("form");
        const taskId = button.dataset.taskId;
        const row = document.getElementById("task-row-" + taskId);

        Swal.fire({
            title: "Are you sure?",
            text: "This task will be deleted!",
            icon: "warning",
            showCancelButton: true,
            confirmButtonColor: "#28a745",
            cancelButtonColor: "#6c757d",
            confirmButtonText: "Yes, delete it!"
        }).then(function (result) {

            if (result.isConfirmed) {

                row.classList.add("fade-out");

                setTimeout(function () {
                    form.submit();
                }, 400);

            }

        });

    }

});