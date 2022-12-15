import { useEffect, useState } from 'react';
import { DataGrid } from '@mui/x-data-grid';
import { Typography } from '@mui/material';

const getDateOfBirth = (params) => new Date(params).toLocaleDateString('en-GB');

const columns = [
  {
      headerName: "Id",
      field: "id",
      flex: 1
  }, 
  {
      headerName: "First Name",
      field: "firstName",
      flex: 1
  }, 
  {
      headerName: "Last Name",
      field: "lastName",
      flex: 1
  }, 
  {
      headerName: "Date of Birth",
      field: "dateOfBirth",
      flex: 1,
      type: "date",
      valueFormatter: ({ value }) => getDateOfBirth(value),
  }
]

const PatientList = ({ clinic }) => {

  const [ patients, setPatients ] = useState();

  useEffect(() => {

    const fetchPatients = async () => {
        const response = await fetch(`https:localhost:7145/clinics/${clinic.id}/patients`);
        const json = await response.json();
        setPatients(json);
      }

      if (!clinic) {
        return;
      }

      fetchPatients()
        .catch(console.error);

  }, [clinic])

if (!patients) return;

  return (
    <div className="PatientList">

      <Typography variant='h2' gutterBottom> Patient list for clinic: {clinic.name}</Typography>

      <DataGrid 
        columns={columns} 
        rows={patients} 
        autoHeight={true}
        aria-label={`Patient list for clinic: ${clinic.name}`}>
      </DataGrid>
    </div>
  );
}

export default PatientList;