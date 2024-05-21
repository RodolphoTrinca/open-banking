import * as React from "react";
import PropTypes from "prop-types";
import { alpha } from "@mui/material/styles";
import MuiToolbar from "@mui/material/Toolbar";
import Typography from "@mui/material/Typography";
import IconButton from "@mui/material/IconButton";
import Tooltip from "@mui/material/Tooltip";
import {
  Delete as DeleteIcon,
  Edit as EditIcon,
  Add as AddIcon,
} from "@mui/icons-material";

const Toolbar = (props) => {
  const { numSelected, name, onEdit, onDelete, onAdd } = props;

  return (
    <MuiToolbar
      sx={{
        pl: { sm: 2 },
        pr: { xs: 1, sm: 1 },
        ...(numSelected > 0 && {
          bgcolor: (theme) =>
            alpha(
              theme.palette.primary.main,
              theme.palette.action.activatedOpacity
            ),
        }),
      }}
    >
      {numSelected > -1 ? (
        <Typography
          sx={{ flex: "1 1 100%" }}
          color="inherit"
          variant="subtitle1"
          component="div"
        >
          {numSelected} selected
        </Typography>
      ) : (
        <Typography
          sx={{ flex: "1 1 100%" }}
          variant="h6"
          id="tableTitle"
          component="div"
        >
          {name}
        </Typography>
      )}

      {numSelected > -1 ? (
        <>
          <Tooltip title="Edit">
            <IconButton onClick={onEdit}>
              <EditIcon/>
            </IconButton>
          </Tooltip>
          <Tooltip title="Delete">
            <IconButton onClick={onDelete}>
              <DeleteIcon/>
            </IconButton>
          </Tooltip>
        </>
      ) : (
        <Tooltip title="Add">
          <IconButton onClick={onAdd}>
            <AddIcon/>
          </IconButton>
        </Tooltip>
      )}
    </MuiToolbar>
  );
};

Toolbar.propTypes = {
  numSelected: PropTypes.number.isRequired,
};

export default Toolbar;
