import React, { useState } from "react";
import { styled } from '@mui/material/styles';
import Paper from '@mui/material/Paper';
import ParticipantCard from "../Card/ParticipantCard";
import { useGetParticipants } from "../../Hooks/Participants/useGetParticipants";
import CircularProgress from '@mui/material/CircularProgress';
import Box from '@mui/material/Box';
import Grid from '@mui/material/Grid';
import Pagination from '@mui/material/Pagination';

const Item = styled(Paper)(({ theme }) => ({
    backgroundColor: theme.palette.mode === 'dark' ? '#1A2027' : '#fff',
    ...theme.typography.body2,
    padding: theme.spacing(1),
    textAlign: 'center',
    color: theme.palette.text.secondary,
}));

const itemsPerPage = 24;

const Participants = () => {
    const [participants, errors, isLoading, refetchParticipants] = useGetParticipants();
    const [page, setPage] = useState(1);
    const handleChange = (event, value) => {
        setPage(value);
    };
    
    if(participants == null){
        return (
            <Box sx={{ display: 'flex' }}>
                <CircularProgress />
            </Box>
        );
    }
        
    const maxPages = (Math.ceil(participants.length / itemsPerPage)) - 1;
    console.log(participants);

    return(
        <Box sx={{ width: '100%' }}>
            <Grid container spacing={{ xs: 2, md: 3 }} columns={{ xs: 1, sm: 8, md: 12 }}>
                {participants
                    .slice(page * itemsPerPage, page * itemsPerPage + itemsPerPage)
                    .map(item => {
                        return (
                        <Grid key={item.id} item xs={1} sm={4} md={4}>
                            <Item>
                                <ParticipantCard data={item}/>
                            </Item>
                        </Grid>);
                })}
            </Grid>
            <Pagination sx={{ display: 'flex'}} count={maxPages} page={page} onChange={handleChange}/>
        </Box>
    );
}

export default Participants;