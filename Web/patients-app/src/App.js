import React, { useEffect, useMemo, useState } from 'react';
import PatientList from './PatientList';
import { Box, Tab, Tabs } from '@mui/material';

const App = () => {  

  const [ clinics, setClinics ] = useState();
  const [ selectedClinicId, setSelectedClinicId ] = useState(false);
  const selectedClinic = useMemo(
    () => clinics?.find(clinic => clinic.id === selectedClinicId),
    [clinics, selectedClinicId]
  );

  useEffect(() => {
    const fetchClinics = async () => {
      const response = await fetch('https:localhost:7145/clinics');
      const clinics = await response.json();
      setClinics(clinics);
    }
    fetchClinics()
      .catch(console.error);
  }, [])

  const selectTab = (event, newValue) => {
    setSelectedClinicId(newValue);
  }

  if (!clinics) return (<div role="placeholder" name="placeholder" />);

  return (
    <div id='students.title' className="App" title='app'>
      <Box sx={{ borderBottom: 1, borderColor: 'divider' }}>
        <Tabs value={selectedClinicId} onChange={selectTab} aria-label="basic tabs">
          {clinics.map(clinic => (
            <Tab value={clinic.id} key={clinic.id} label={clinic.name} />
          ))}
        </Tabs>
      </Box>
      <PatientList clinic={selectedClinic} />
    </div>
  );
}

export default App;