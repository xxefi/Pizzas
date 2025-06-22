import { Avatar } from "rsuite";

export const AvatarComponent = ({
  user,
  initials,
}: {
  user: any;
  initials: string;
}) => (
  <Avatar
    circle
    alt={user?.firstName}
    src={user?.profilePicture}
    className="w-10 h-10 ring-2 ring-white ring-offset-2 ring-offset-blue-400 transition-all duration-200 hover:scale-105 shadow-md"
    style={{
      background: !user?.Picture
        ? "linear-gradient(135deg, #4F46E5 0%, #7C3AED 100%)"
        : undefined,
    }}
  >
    {!user?.Picture && (
      <span className="text-white text-sm font-medium">{initials}</span>
    )}
  </Avatar>
);
