import * as React from "react";
import Box from "@mui/material/Box";
import MuiTable from "@mui/material/Table";
import TableContainer from "@mui/material/TableContainer";
import TablePagination from "@mui/material/TablePagination";
import Paper from "@mui/material/Paper";
import Header from "./Header";
import Toolbar from "./Toolbar";
import Body from "./Body";

const Table = (props) => {
  const { headCells, rows, onClickEdit, onClickDelete, onClickAdd } = props;
  const rowsPerPageOptions = [25, 50, 75];

  const [order, setOrder] = React.useState("asc");
  const [orderBy, setOrderBy] = React.useState("");
  const [selected, setSelected] = React.useState(-1);
  const [page, setPage] = React.useState(0);
  const [rowsPerPage, setRowsPerPage] = React.useState(rowsPerPageOptions[0]);

  const handleRequestSort = (event, property) => {
    const isAsc = orderBy === property && order === "asc";
    setOrder(isAsc ? "desc" : "asc");
    setOrderBy(property);
  };

  const handleChangePage = (event, newPage) => {
    setPage(newPage);
  };

  const handleChangeRowsPerPage = (event) => {
    setRowsPerPage(parseInt(event.target.value, 10));
    setPage(0);
  };

  const onEdit = () => {
    setSelected(-1);
    onClickEdit(selected);
  };

  const onDelete = () => {
    setSelected(-1);
    onClickDelete(selected);
  };

  return (
    <Box sx={{ width: "100%" }}>
      <Paper sx={{ width: "100%", mb: 2 }}>
        <Toolbar
          name={props.name}
          numSelected={selected}
          onAdd={onClickAdd}
          onEdit={onEdit}
          onDelete={onDelete}
        />
        <TableContainer>
          <MuiTable sx={{ minWidth: 750 }} aria-labelledby="tableTitle">
            <Header
              order={order}
              orderBy={orderBy}
              onRequestSort={handleRequestSort}
              headCells={headCells}
            />
            <Body
              rows={rows}
              order={order}
              orderBy={orderBy}
              page={page}
              rowsPerPage={rowsPerPage}
              selected={selected}
              setSelected={setSelected}
              headCells={headCells}
            />
          </MuiTable>
        </TableContainer>
        <TablePagination
          rowsPerPageOptions={rowsPerPageOptions}
          component="div"
          count={rows.length}
          rowsPerPage={rowsPerPage}
          page={page}
          onPageChange={handleChangePage}
          onRowsPerPageChange={handleChangeRowsPerPage}
        />
      </Paper>
    </Box>
  );
};

export default Table;
