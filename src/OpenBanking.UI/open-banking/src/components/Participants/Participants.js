import { Table } from "@mui/material";
import React, {useState} from "react";

const pageName = "Participants"

const Participants = () => {
    return (
        <Table
            name={pageName}
            rows={data}
            headCells={headCells}
        />
    )
}

export default Participants;