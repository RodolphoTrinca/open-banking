import { Box, Container, Table } from '@mui/material';
import './App.css';
import DrawerAppBar from './components/NavBar/DrawerAppBar';

function App() {
  return (
    <main>
      <DrawerAppBar/>
      <Box component="main" sx={{ p: 6 }}>
        <Container>
          <Table/>
        </Container>
      </Box>
    </main>
  );
}

export default App;
