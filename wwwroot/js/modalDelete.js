function showConfirmDelete(e, id) {
    e.preventDefault();
    Swal.fire({
        title: 'Confirm Deletion',
        text: 'Are you sure you want to delete this employee? This action cannot be undone.',
        showCancelButton: true,
        confirmButtonText: 'Delete',
        confirmButtonColor: 'red',
        reverseButtons: true,
        backdrop: `rgba(0, 0, 0, 0.8)`,
        width: 610,
        customClass: {
            popup: 'p-3',
            title: 'fw-bold fs-4 text-dark text-start p-2',
            htmlContainer: 'text-start px-2 py-1',
            actions: 'justify-content-end m-2',
          }
      }).then((result) => {
        if (result.isConfirmed) {
            window.location.href = `/Employee/Delete/${id}`
        }
      })
}