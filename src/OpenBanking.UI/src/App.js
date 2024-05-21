import { Box, Container, Table } from '@mui/material';
import './App.css';
import DrawerAppBar from './components/NavBar/DrawerAppBar';
import Participants from './components/Participants/Participants';

function App() {
  return (
    <main>
      <DrawerAppBar/>
      <Box component="main" sx={{ p: 6 }}>
        <Container>
          <Participants/>
        </Container>
      </Box>
    </main>
  );
}

export default App;
