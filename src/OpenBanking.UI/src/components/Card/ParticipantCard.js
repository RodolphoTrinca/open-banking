import * as React from 'react';
import { styled } from '@mui/material/styles';
import Card from '@mui/material/Card';
import CardHeader from '@mui/material/CardHeader';
import CardMedia from '@mui/material/CardMedia';
import CardContent from '@mui/material/CardContent';
import CardActions from '@mui/material/CardActions';
import Collapse from '@mui/material/Collapse';
import Avatar from '@mui/material/Avatar';
import IconButton from '@mui/material/IconButton';
import Typography from '@mui/material/Typography';
import ExpandMoreIcon from '@mui/icons-material/ExpandMore';
import { Link } from '@mui/material';

const ExpandMore = styled((props) => {
  const { expand, ...other } = props;
  return <IconButton {...other} />;
})(({ theme, expand }) => ({
  transform: !expand ? 'rotate(0deg)' : 'rotate(180deg)',
  marginLeft: 'auto',
  transition: theme.transitions.create('transform', {
    duration: theme.transitions.duration.shortest,
  }),
}));

const companyLogo = styled(`
    width: '100%',
    max-width: '128px'
`);

export default function ParticipantCard(props) {
  const {data} = props;
  const [expanded, setExpanded] = React.useState(false);

  const handleExpandClick = () => {
    setExpanded(!expanded);
  };

  return (
    <Card sx={{ maxWidth: 345 }}>
      <CardHeader
        avatar={
          <Avatar aria-label="recipe" src={data.autorizationServers.logoURI}/>
        }
        title={data.name}
      />
      <CardContent>
        <Typography variant="body1">Configuration URL:</Typography>
        <Typography color="text.secondary">
          {data.autorizationServers.configurationURL ? (
              <Link 
                href={data.autorizationServers.configurationURL} 
                variant="body2" 
                underline="hover" 
                target="_blank" 
                rel="noopener">
                  {data.autorizationServers.configurationURL}
              </Link>) : ("Null")
          }
        </Typography>
      </CardContent>
      <CardActions disableSpacing>
        <ExpandMore
          expand={expanded}
          onClick={handleExpandClick}
          aria-expanded={expanded}
          aria-label="show more"
        >
          <ExpandMoreIcon />
        </ExpandMore>
      </CardActions>
      <Collapse in={expanded} timeout="auto" unmountOnExit>
        <CardContent>
          <Typography paragraph>Discovery Authorization:</Typography>
          <Typography paragraph color="text.secondary">
            {data.autorizationServers.discoveryAuthorization}
          </Typography>
        </CardContent>
      </Collapse>
    </Card>
  );
}