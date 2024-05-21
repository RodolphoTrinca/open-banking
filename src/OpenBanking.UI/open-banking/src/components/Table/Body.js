import * as React from "react";
import TableBody from "@mui/material/TableBody";
import TableCell from "@mui/material/TableCell";
import TableRow from "@mui/material/TableRow";

function descendingComparator(a, b, orderBy) {
  if (b[orderBy] < a[orderBy]) {
    return -1;
  }
  if (b[orderBy] > a[orderBy]) {
    return 1;
  }
  return 0;
}

function getComparator(order, orderBy) {
  return order === "desc"
    ? (a, b) => descendingComparator(a, b, orderBy)
    : (a, b) => -descendingComparator(a, b, orderBy);
}

const Body = (props) => {
  const {
    rows,
    order,
    orderBy,
    page,
    rowsPerPage,
    selected,
    setSelected,
    headCells,
  } = props;

  const handleClick = (event, index) => {
    if (index === selected) {
      setSelected(-1);
      return;
    }

    setSelected(index + (page * rowsPerPage));
  };

  const isSelected = (index) => selected === index;

  // Avoid a layout jump when reaching the last page with empty rows.
  const emptyRows =
    page > 0 ? Math.max(0, (1 + page) * rowsPerPage - rows.length) : 0;

  return (
    <TableBody>
      {rows
        .sort(getComparator(order, orderBy))
        .slice(page * rowsPerPage, page * rowsPerPage + rowsPerPage)
        .map((row, index) => {
          const isItemSelected = isSelected(index);
          const labelId = `enhanced-table-checkbox-${index}`;

          return (
            <TableRow
              hover
              onClick={(event) => handleClick(event, index)}
              aria-checked={isItemSelected}
              key={index}
              selected={isItemSelected}
            >
              {headCells.map((head, index) => {
                if (index === 0) {
                  return (
                    <TableCell
                      component="th"
                      id={labelId}
                      key={head.id + "_" + index}
                      scope="row"
                    >
                      {row[head.id]}
                    </TableCell>
                  );
                }

                var formatedData = row[head.id];
                if(head.format){
                  formatedData = head.format(row[head.id]);
                }

                return <TableCell key={head.id + "_" + index} align="left">{formatedData}</TableCell>;
              })}
            </TableRow>
          );
        })}
      {emptyRows > 0 && (
        <TableRow
          style={{
            height: 53 * emptyRows,
          }}
        >
          <TableCell colSpan={6} />
        </TableRow>
      )}
    </TableBody>
  );
};

export default Body;
