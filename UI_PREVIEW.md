# Enquiry Management System - UI Preview

## Color Palette

### Primary Colors
```
Primary Blue:     #0066cc (Professional, trustworthy)
Primary Hover:    #0052a3 (Darker shade on hover)
```

### Status Colors
```
Success (Green):  #28a745 (Active, approved)
Danger (Red):     #dc3545 (Error, delete, urgent)
Warning (Yellow): #ffc107 (Caution)
Info (Teal):      #17a2b8 (Information)
```

### Neutral Colors
```
Light Background: #f8f9fa (Page background)
White Surface:    #ffffff (Cards, forms)
Border:           #dee2e6 (Subtle dividers)
Dark Text:        #212529 (Body text)
Muted Text:       #6c757d (Secondary text)
```

---

## Page Layouts

### 1. HOME PAGE `/`

```
┌────────────────────────────────────────────────────────┐
│                   Welcome Header                        │
│  Welcome to Enquiry Management System                  │
│  Manage your business enquiries efficiently...         │
└────────────────────────────────────────────────────────┘

┌──────────────────────────┐  ┌──────────────────────────┐
│   Getting Started        │  │   Key Features           │
│                          │  │                          │
│  • View Dashboard    ┌─┐ │  │ ✓ Create & manage enq...│
│  • Manage Enquiries  └─┘ │  │ ✓ Track customers...    │
│                          │  │ ✓ Manage enquiry items..│
│  [View Dashboard]        │  │ ✓ Dashboard statistics..│
│  [Manage Enquiries]      │  │ ✓ Light-themed UI       │
│                          │  │ ✓ Supabase database     │
└──────────────────────────┘  └──────────────────────────┘
```

### 2. DASHBOARD PAGE `/dashboard`

```
┌────────────────────────────────────────────────────────┐
│                   Dashboard Header                      │
│  Enquiry Management System Overview                    │
└────────────────────────────────────────────────────────┘

┌─────────────┐ ┌─────────────┐ ┌─────────────┐ ┌─────────────┐
│ 24          │ │ 8           │ │ 4           │ │ 2           │
│ TOTAL       │ │ PENDING     │ │ ACTIVE      │ │ ACTIVE      │
│ ENQUIRIES   │ │ QUOTATIONS  │ │ COMPANIES   │ │ USERS       │
│ +12% month  │ │ Due: Dec 21 │ │ +2 new week │ │ Sales Eng.  │
└─────────────┘ └─────────────┘ └─────────────┘ └─────────────┘

┌─ Recent Enquiries ─────────┐  ┌─ Top Customers ────────────┐
│ Request  │ Client │ Date │ St│  │ Company │ Category│Phone │ St│
│ EYS/001  │ Cust X │ 11/11│En│  │ Cust X  │ Contrc │ 222 │ Ac│
│ EYS/002  │ Cust Y │ 11/13│Qu│  │ Cust Y  │ Contrc │ 555 │ Ac│
│ EYS/003  │ Cust Z │ 11/15│En│  │ Client Z│ Client │ 888 │ Ac│
└────────────────────────────┘  └────────────────────────────┘

┌─ Sales Engineers Performance ─────────────────────────────┐
│ Name          │ Email        │ Dept │ Status    │ Roles  │
│ John Doe      │ se1@comp.com │ MEP  │ Active    │ Enq,Qt │
│ Jane Smith    │ se2@comp.com │ MEP  │ Active    │ Enq,Ad │
└───────────────────────────────────────────────────────────┘
```

### 3. ENQUIRY MANAGEMENT PAGE `/ems`

```
┌────────────────────────────────────────────────────────┐
│            Enquiry Management System                    │
└────────────────────────────────────────────────────────┘

[New Enquiry] [Modify Enquiry] [Search Enquiry]
     ↓

═══ NEW ENQUIRY TAB ═══

Section 1: Enquiry Basics
┌─ Source of Enquiry ──┐
│ [  Email      ▼  ]  │
└──────────────────────┘

┌─ Enquiry Date ──┐ ┌─ Due Date ──┐ ┌─ Site Visit Date ──┐
│ [2025-11-16]    │ │ [2025-12-16]│ │ [optional]         │
└─────────────────┘ └─────────────┘ └────────────────────┘

Section 2: Enquiry Classification
┌─ Enquiry Type ───────────────┐
│ [  Select Type    ▼      ]   │
│ ┌─────────────────────────┐  │
│ │ Electrical              │  │
│ │ Mechanical              │  │
│ │ Civil                   │  │
│ └─────────────────────────┘  │
│ Selected: [Electrical] [+] [-]│
└──────────────────────────────┘

┌─ Enquiry For ────────────────┐
│ [Item search...] [New] [Edit]│
│ ┌─────────────────────────┐  │
│ │ Electrical              │  │
│ │ Mechanical              │  │
│ └─────────────────────────┘  │
│ Selected: [Electrical] [+] [-]│
└──────────────────────────────┘

Section 3: Party Details
┌─ Customer Company ─────────┐
│ [Select Company ▼] [N] [E] │
│ ┌──────────────────────┐   │
│ │ Cust X              │   │
│ │ Cust Y              │   │
│ └──────────────────────┘   │
│ Selected: [Cust X] [+] [-] │
└────────────────────────────┘

┌─ Contact Person ──────────┐
│ [Select Contact ▼][N][E]  │
│ ┌──────────────────────┐  │
│ │ Velu (Cust X)       │  │
│ │ Vijay (Cust Y)      │  │
│ └──────────────────────┘  │
│ Selected: [Velu] [+] [-]  │
└───────────────────────────┘

┌─ Project Name ─────────────────┐
│ [Project search or new...] ▼   │
└────────────────────────────────┘

┌─ Client Name ────────────────────┐
│ [Client search...] [New] [Edit]  │
└─────────────────────────────────┘

┌─ Consultant Name ────────────────┐
│ [Consultant search...] [New]     │
└─────────────────────────────────┘

┌─ Sales Engineer ─────────────────┐
│ [SE search...] [New] [Edit]      │
│ ┌────────────────────────────┐   │
│ │ John Doe (se1@comp.com)   │   │
│ │ Jane Smith (se2@comp.com) │   │
│ └────────────────────────────┘   │
│ Selected: [John Doe] [+] [-]     │
└─────────────────────────────────┘

Section 4: Details & Documents
┌─ Enquiry Details ─────────────┐
│                               │
│ [Large text area for details] │
│ [.....................]      │
│                               │
└───────────────────────────────┘

┌─ Documents Received ──────────────────┐
│ ☐ Hard Copies                        │
│ ☐ Drawings                           │
│ ☐ CD/DVD                             │
│ ☐ Specification                      │
│ ☐ Equipment Schedule                 │
│ Others: [text area......]            │
└───────────────────────────────────────┘

Section 5: Additional Options
┌─ Remarks ─────────────────────┐
│ [Text area for remarks...]    │
└───────────────────────────────┘

☐ Send acknowledgement mail to customer?
☐ Require ED/CEO Signature?

[Add] [Cancel]

═══ MODIFY ENQUIRY TAB ═══

┌─ Request No ───────────┐
│ [EYS/2025/11/001] [Load] [Clear]
└────────────────────────┘

Status: Loaded EYS/2025/11/001. You can edit below...

[Edit form similar to above with loaded data]

[Save Changes] [Cancel]

═══ SEARCH ENQUIRY TAB ═══

┌─ Search Text ──────────┐ ┌─ From Date ────┐ ┌─ To Date ────┐ ┌─ Actions ─┐
│ [Search...] │ │ [2025-01-01] │ │ [2025-12-31] │ │ [Search]   │
└────────────────────────┘ └────────────────┘ └──────────────┘ │ [Clear]   │
                                                               └───────────┘

Results Table:
┌──────────────────────────────────────────────────────────────┐
│ Request  │ Date │ Client │ Project │ Source │ Due │ SE │ Act│
│ EYS/001  │ 11/11│ Cust X │ Proj A  │ Email  │ 12/1│ JD │ Opn│
│ EYS/002  │ 11/13│ Cust Y │ Proj B  │ Phone  │ 12/8│ JS │ Opn│
│ EYS/003  │ 11/15│ Cust Z │ Proj C  │ Online │ 12/5│ JD │ Opn│
└──────────────────────────────────────────────────────────────┘
```

---

## Modal Dialogs

### Customer/Client/Consultant Modal

```
┌─────────────────────────────────────────────────────────────┐
│ Customer/Client/Consultant Details (Add New)         [×]   │
├─────────────────────────────────────────────────────────────┤
│                                                             │
│ ┌─ Category ─────────┐  ┌─ Company Name ────────────────┐ │
│ │ Contractor    ▼    │  │ [Company name here]           │ │
│ └────────────────────┘  └───────────────────────────────┘ │
│                                                             │
│ ┌─ Address 1 ────────────┐  ┌─ Address 2 ────────────────┐ │
│ │ [Address line 1]       │  │ [Address line 2]           │ │
│ └────────────────────────┘  └────────────────────────────┘ │
│                                                             │
│ ┌─ Phone 1 ──────┐ ┌─ Phone 2 ──────┐ ┌─ Fax ──────────┐ │
│ │ [Phone]        │ │ [Phone]        │ │ [Fax]          │ │
│ └────────────────┘ └────────────────┘ └────────────────┘ │
│                                                             │
│ ┌─ Email ────────────────────┐  ┌─ Website ──────────────┐ │
│ │ [company@example.com]      │  │ [www.example.com]      │ │
│ └────────────────────────────┘  └────────────────────────┘ │
│                                                             │
│ ┌─ Status ───────────┐  ┌─ Type ────────────────────────┐  │
│ │ Active        ▼    │  │ [Type] [Type] [Type]  ▼       │  │
│ └────────────────────┘  └───────────────────────────────┘  │
│                                                             │
├─────────────────────────────────────────────────────────────┤
│                                 [Add]  [Cancel]             │
└─────────────────────────────────────────────────────────────┘
```

---

## Component Styling

### Form Cards
- Background: White (#ffffff)
- Border: 1px solid #dee2e6
- Padding: 2rem
- Border-radius: 0.5rem
- Box-shadow: 0 4px 6px rgba(0,0,0,0.1)
- Margin-bottom: 1.5rem

### Form Labels
- Font-weight: 500
- Font-size: 0.875rem
- Color: #212529
- Margin-bottom: 0.5rem

### Form Inputs
- Border: 1px solid #ced4da
- Border-radius: 0.375rem
- Padding: 0.5rem 0.75rem
- Font-size: 0.875rem
- Transition: 0.2s ease

### Form Inputs (Focus)
- Border-color: #0066cc
- Box-shadow: 0 0 0 0.2rem rgba(0, 102, 204, 0.15)

### Buttons
- Padding: 0.5rem 1.25rem
- Border-radius: 0.375rem
- Font-weight: 500
- Font-size: 0.875rem
- Transition: all 0.2s ease

### Button: Primary
- Background: #0066cc
- Color: white
- Hover: #0052a3 + shadow lift

### Button: Outline
- Background: transparent
- Color: #0066cc
- Border: 1px solid #0066cc
- Hover: Filled with color

### Tables
- Background: white
- Header background: #f8f9fa
- Padding: 1rem
- Border-collapse: collapse
- Border-bottom: 1px solid #dee2e6

### Badges
- Padding: 0.375rem 0.75rem
- Font-size: 0.75rem
- Font-weight: 600
- Border-radius: 0.25rem
- Uppercase
- Success (green), Danger (red), Info (teal)

### Stats Cards
- Background: Linear gradient white → light gray
- Padding: 1.5rem
- Border-radius: 0.5rem
- Box-shadow: 0 4px 6px rgba(0,0,0,0.1)
- Hover: Lift up 2px + enhanced shadow

---

## Responsive Behavior

### Desktop (1200px+)
- Sidebar: 240px fixed
- Main content: Flexible
- Columns: Full width available
- Modals: 800px max-width

### Tablet (768px - 1199px)
- Sidebar: Visible but narrower
- Columns: 2-column layout when possible
- Modals: 90% width

### Mobile (<768px)
- Sidebar: Collapsible or bottom navigation
- Columns: Single column
- Modals: Full screen minus padding
- Tables: Horizontal scroll

---

## Animations

### Transitions
- All: 0.2s ease (standard)
- Buttons: Lift effect on hover
- Forms: Smooth focus effects
- Tables: Row highlight on hover

### Page Loads
- Fade in: 0.3s ease-in
- Stagger: List items with slight delay

### Interactions
- Button clicks: Subtle press effect
- Hover states: Color shift and/or lift
- Focus states: Border color + glow

---

## Accessibility Features

- **Color Contrast**: WCAG AA compliant
- **Focus States**: Visible keyboard navigation
- **Labels**: Associated with form inputs
- **Semantic HTML**: Proper heading hierarchy
- **ARIA Attributes**: Added for complex components
- **Mobile Touch**: Adequate button/link sizing (44px minimum)

---

This modern, professional UI delivers:
✓ Clean, minimalist aesthetic
✓ Intuitive navigation
✓ Consistent visual language
✓ Professional light color scheme
✓ Smooth interactions
✓ Responsive across all devices
✓ Accessible to all users
