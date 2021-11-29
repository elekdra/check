import DataTable from 'react-data-table-component';
import pdf from '../../../assets/pdf.png';
import edit from '../../../assets/edit.png';
import deleted from '../../../assets/deleted.png';
import { useHistory } from 'react-router';
import './DocumentTable.css';
import { useState, useEffect } from 'react';

const paginationComponentOptions = {
  rowsPerPageText: 'No of Rows per page',
  rangeSeparatorText: 'of',
  selectAllRowsItem: true,
};
function DocumentTable(props) {
  const [data, setData] = useState();
  const history = useHistory();
  const columns = [
    {
      name: 'Name',
      selector: (row) => row.fileName,
      sortable: true,
    },
    {
      name: 'Training',
      selector: (row) => row.training,
      sortable: true,
    },
    {
      name: 'Version',
      selector: (row) => row.version,
      sortable: true,
    },
    {
      name: 'Company',
      selector: (row) => row.company,
      sortable: true,
    },
    {
      name: 'Action',
      sortable: true,
      cell: (row) => (
        <div>
          <a
            href={row.fileContent}
            id={row.ID}
            target='_blank'
            rel='noreferrer'
          >
            <img src={pdf} alt='' />
          </a>

          <button
            style={{ backgroundColor: 'white' }}
            className='upload-button'
            onClick={() => {
              history.push('/uploaddocument', {
                Company: row.company,
                Version: row.version,
                Training: row.training,
                FileName: row.fileName,
                isNotEditable: true,
              });
            }}
          >
            {' '}
            <img src={edit} alt='' />
          </button>

          <button
            style={{ border: 'none' }}
            onClick={() =>
              deleteFile(row.fileName, row.version, row.company, row.training)
            }
          >
            <img src={deleted} alt='' />
          </button>
        </div>
      ),
      ignoreRowClick: true,
      allowOverflow: true,
      button: true,
    },
  ];
  function deleteFile(fileName, fileVersion, fileCompany, training) {
    var userPreference;

    //eslint-disable-next-line no-restricted-globals
    if (confirm('Deleted File Cant be Restored?') === true) {
      userPreference = 'Data saved successfully!';

      var xhttp = new XMLHttpRequest();
      xhttp.onreadystatechange = function () {
        if (this.readyState === 4 && this.status === 200) {
          props.onDeleted();
        }
      };
      xhttp.open(
        'GET',
        'http://localhost:5000/api/task/filedelete?file=' +
          fileName +
          '|' +
          fileVersion +
          '|' +
          fileCompany +
          '|' +
          training,
        true
      );
      xhttp.send();
    } else {
      userPreference = 'Save Cancelled!';
    }
  }

  const handleData = () => {
    setData(props.fullData);
  };

  useEffect(() => {
    handleData();
  }, [handleData]);
  console.log(props.fullData);
  return (
    <div className='datatable-items'>
      <DataTable
        handle
        columns={columns}
        data={data}
        pagination
        paginationComponentOptions={paginationComponentOptions}
      />
    </div>
  );
}

export default DocumentTable;
